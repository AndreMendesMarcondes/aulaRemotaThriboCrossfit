using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AulaRemotaThriboCrossfit.Models
{
    [Table("Treino")]
    public class Treino
    {
        [Key]
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Equipamento { get; set; }
        public string VideoURL { get; set; }
    }
}
