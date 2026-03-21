using ControleGastos.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ControleGastos.Core.Models
{
    [Table("Categorias")]
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

       
        [MaxLength(400, ErrorMessage = "A Descrição deve ter máximo 400 caracteres")]

        public string Descricao { get; set; }

        public FinalidadeCategoria Finalidade { get; set; }
    }
}
