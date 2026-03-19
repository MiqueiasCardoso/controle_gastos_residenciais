using ControleGastos.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleGastos.Core.Models
{
    [Table("Transacoes")]
    public class Transacao
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(400)]
        public String Descricao { get; set; }

        public decimal Valor { get; set; }
        
        public TipoTransacao Tipo { get; set; }

        public int CategoriaId { get; set; }

        public int PessoaId { get; set; }
    }
}
