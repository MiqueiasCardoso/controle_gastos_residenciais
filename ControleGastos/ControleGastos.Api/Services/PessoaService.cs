using ControleGastos.Api.Repositories;
using ControleGastos.Core.Models;

namespace ControleGastos.Api.Services
{
    public class PessoaService
    {
        private readonly PessoaRepository _pessoaRepository;

        public PessoaService(PessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
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
            throw new NotImplementedException();
        }       
        public async Task<Pessoa> RemoverPessoa(int id, Pessoa pessoa)
        {
            throw new NotImplementedException();
        }
    }
}
