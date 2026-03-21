using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleGastos.Core.Models
{
    [Table("Pessoas")]
    public class Pessoa
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(200, ErrorMessage = "O nome deve ter no máximo 200 caracteres")]
        public String Nome { get; set; }

        public int Idade { get; set; }
    }
}
