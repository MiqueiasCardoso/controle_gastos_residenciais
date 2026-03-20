using ControleGastos.Core.Enums;

namespace ControleGastos.Api.Utils
{
    public static class MetodosUteis
    {
        public static bool CategoriaValidaParaTipo(TipoTransacao tipo, FinalidadeCategoria finalidade)
        {
            if (tipo == TipoTransacao.Despesa)
            {
                return finalidade == FinalidadeCategoria.Despesa
                    || finalidade == FinalidadeCategoria.Ambas;
            }

            if (tipo == TipoTransacao.Receita)
            {
                return finalidade == FinalidadeCategoria.Receita
                    || finalidade == FinalidadeCategoria.Ambas;
            }

            return false;
        }
    }
}
