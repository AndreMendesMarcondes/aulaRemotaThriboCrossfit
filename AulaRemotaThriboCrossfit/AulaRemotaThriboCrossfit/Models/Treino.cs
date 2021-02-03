using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AulaRemotaThriboCrossfit.Models
{
    [Table("Treino")]
    public class Treino
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Dia { get; set; }
        public List<ExercicioDescricao> Exercicios { get; set; }
    }
}
