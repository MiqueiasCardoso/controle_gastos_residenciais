using ControleGastos.Api.Repositories;
using ControleGastos.Api.Utils;
using ControleGastos.Core.CoreViewModels;
using ControleGastos.Core.Enums;
using ControleGastos.Core.Models;

namespace ControleGastos.Api.Services
{
    public class TransacaoService
    {
        private readonly TransacaoRepository _transacaoRepository;
        private readonly CategoriaRepository _categoriaRepository;
        private readonly PessoaRepository _pessoaRepository;

        public TransacaoService(TransacaoRepository transacaoRepository, CategoriaRepository categoriaRepository, PessoaRepository pessoaRepository)
        {
            _transacaoRepository = transacaoRepository;
            _categoriaRepository = categoriaRepository;
            _pessoaRepository = pessoaRepository;
        }
        public async Task<List<Transacao>> BuscarTransacaos()
        {
            var transacaos = await _transacaoRepository.Get();
            return transacaos;
        }
        public async Task<Transacao> BuscarTransacaoPorId(int id)
        {
            var transacao = await _transacaoRepository.GetById(id);
            return transacao;
        }
        public async Task<Transacao> InserirTransacao(Transacao transacao)
        {
            var pessoa = await _pessoaRepository.GetById(transacao.PessoaId);
            if (pessoa == null)
                throw new Exception("Pessoa não encontrada");

            var categoria = await _categoriaRepository.GetById(transacao.CategoriaId);
            if (categoria == null)
                throw new Exception("Categoria não encontrada");

            // REGRA 1: menor de idade
            if (pessoa.Idade < 18 && transacao.Tipo == TipoTransacao.Receita)
            {
                throw new Exception("Menores de idade só podem ter despesas");
            }

            // REGRA 2: categoria compatível com tipo
            if (!MetodosUteis.CategoriaValidaParaTipo(transacao.Tipo, categoria.Finalidade))
            {
                throw new Exception("Categoria incompatível com o tipo da transação");
            }

            // REGRA 3: valor positivo
            if (transacao.Valor <= 0)
            {
                throw new Exception("Valor deve ser positivo");
            }

            await _transacaoRepository.Insert(transacao);
            return transacao;
        }

        //Reports

        public async Task<List<ResumoFinanceiroPessoaViewModel>> BuscarResumoFinanceiroPorPessoa()
        {
            return await _transacaoRepository.GetResumoFinanceiroByPessoa();
        }
        
        public async Task<List<ResumoFinanceiroCategoriaViewModel>> BuscarResumoFinanceiroPorCategoria()
        {
            return await _transacaoRepository.GetResumoFinanceiroByCategoria();
        }
    }
}
