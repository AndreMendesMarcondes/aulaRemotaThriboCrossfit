using System;
using System.ComponentModel.DataAnnotations;

namespace AulaRemotaThriboCrossfit.Models
{
    public class ExercicioDescricao
    {
        [Key]
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public Guid ExercicioId { get; set; }
        public Guid TreinoId { get; set; }
        public virtual Exercicio Exercicio { get; set; }        
    }
}
