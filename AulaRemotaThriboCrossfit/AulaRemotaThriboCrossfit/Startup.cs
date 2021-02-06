using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.Cookies;
using AulaRemotaThriboCrossfit.Data.Imp;
using AulaRemotaThriboCrossfit.Data.Interface;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Net;
using AulaRemotaThriboCrossfit.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using System.Threading;
using System.Globalization;

namespace AulaRemotaThriboCrossfit
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
           .AddCookie(options =>
           {
               options.LoginPath = "/Login/Index";
           });

            services.AddControllersWithViews()
                  .AddRazorRuntimeCompilation();

            AuthenticationWithGoogle(services);

            services.AddScoped<IExercicioRepository, ExercicioRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IFirebaseStorageRepository, FirebaseStorageRepository>();
            services.AddScoped<ITreinoRepository, TreinoRepository>();

            EnableCors(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRequestLocalization(new RequestLocalizationOptions()
            {
                DefaultRequestCulture = new RequestCulture("pt-BR")
            });
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStatusCodePages(async context => {
                var response = context.HttpContext.Response;

                if (response.StatusCode == (int)HttpStatusCode.Unauthorized ||
                    response.StatusCode == (int)HttpStatusCode.NotFound ||
                    response.StatusCode == (int)HttpStatusCode.Forbidden)
                    response.Redirect("/Login/Index");
                else
                {
                    app.UseExceptionHandler("/Home/Index");
                }
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static void AuthenticationWithGoogle(IServiceCollection services)
        {
            string credential_path = $@"{Directory.GetCurrentDirectory()}/GCredentials/aularemotathribocrossfit-690e2c09fd7f.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credential_path);
        }

        private static void EnableCors(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("aularemotathribocrossfit", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
        }
    }
}
