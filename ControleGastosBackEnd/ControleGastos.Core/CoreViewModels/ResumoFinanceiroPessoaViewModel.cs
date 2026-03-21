using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ControleGastos.Core.CoreViewModels
{
    public class ResumoFinanceiroPessoaViewModel
    {
        public int PessoaId { get; set; }

        public String Nome { get; set; }

        public decimal Receitas { get; set; }

        public decimal Despesas { get; set; }

        public decimal Saldo { get; set; }
    }
}
