using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AulaRemotaThriboCrossfit.Models;

namespace AulaRemotaThriboCrossfit.Data
{
    public class AulaRemotaThriboCrossfitContext : DbContext
    {
        public AulaRemotaThriboCrossfitContext (DbContextOptions<AulaRemotaThriboCrossfitContext> options)
            : base(options)
        {
        }

        public DbSet<AulaRemotaThriboCrossfit.Models.Exercicio> Exercicio { get; set; }

        public DbSet<AulaRemotaThriboCrossfit.Models.Treino> Treino { get; set; }
    }
}
