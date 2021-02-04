using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using System;

namespace AulaRemotaThriboCrossfit.Filter
{
    public class AuthorizeFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if (filterContext.HttpContext.Request.Headers.ContainsKey("TokenAuth"))
            {
                
                return;
            }

            filterContext.Result = new RedirectResult("/Login/Index");
        }
    }
}
