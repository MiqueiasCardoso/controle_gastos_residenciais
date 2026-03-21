using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ControleGastos.Core.CoreViewModels
{
    public class ResumoFinanceiroCategoriaViewModel
    {
        public int CategoriaId { get; set; }

        public String Descricao { get; set; }

        public decimal Receitas { get; set; }

        public decimal Despesas { get; set; }

        public decimal Saldo { get; set; }
    }
}
