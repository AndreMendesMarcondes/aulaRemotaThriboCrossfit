using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Http;
using System;

namespace AulaRemotaThriboCrossfit.Models
{
    public class ExercicioDTO : BaseDomain
    {
        public ExercicioDTO()
        {
            Uid = Guid.NewGuid().ToString();
        }
        public string Uid { get; set; }
        public string Nome { get; set; }
        public string Equipamento { get; set; }
        public IFormFile Video { get; set; }
    }
}
