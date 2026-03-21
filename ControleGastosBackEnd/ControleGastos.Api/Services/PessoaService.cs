using ControleGastos.Api.Repositories;
using ControleGastos.Core.Models;

namespace ControleGastos.Api.Services
{
    public class PessoaService
    {
        private readonly PessoaRepository _pessoaRepository;
        private readonly TransacaoRepository _transacaoRepository;

        public PessoaService(PessoaRepository pessoaRepository, TransacaoRepository transacaoRepository)
        {
            _pessoaRepository = pessoaRepository;
            _transacaoRepository = transacaoRepository;
        }
        public async Task<List<Pessoa>> BuscarPessoas()
        {
            var pessoas = await _pessoaRepository.Get();
            return pessoas;
        }
        public async Task<Pessoa> BuscarPessoaPorId(int id)
        {
            var pessoa = await _pessoaRepository.GetById(id);
            return pessoa;
        }
        public async Task<Pessoa> InserirPessoa(Pessoa pessoa)
        {
            await _pessoaRepository.Insert(pessoa);

            return pessoa;
        }
        public async Task<Pessoa> AtualizarPessoa(int id, Pessoa pessoa)
        {
            if (id != pessoa.Id)
            {
                throw new Exception("Id do corpo da requisição diferente do Id da Pessoa");
            }
            await _pessoaRepository.Update(pessoa);
            return pessoa;
        }
        public async Task RemoverPessoa(int id, Pessoa pessoa)
        {
            if (id != pessoa.Id)
            {
                throw new Exception("Id do corpo da requisição diferente do Id da Pessoa");
            }
            await _transacaoRepository.DeleteTransacoesByPessoaId(pessoa.Id);

            await _pessoaRepository.Delete(pessoa);

        }
    }
}
