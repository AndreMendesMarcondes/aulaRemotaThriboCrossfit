using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AulaRemotaThriboCrossfit.Models
{
    [Table("Exercicio")]
    public class Exercicio
    {
        [Key]
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Equipamento { get; set; }
        public string VideoURL { get; set; }
    }
}
