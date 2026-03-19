using ControleGastos.Core.Models;
using Dapper.Contrib.Extensions;
using System.Data;


namespace ControleGastos.Api.Repositories
{
    public class PessoaRepository
    {
        private readonly IDbConnection _connection;

        public PessoaRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<List<Pessoa>> Get()
        {
            var pessoas = await _connection.GetAllAsync<Pessoa>();
            return pessoas.ToList();
        }
        public async Task<Pessoa> GetById(int id)
        {
            return await _connection.GetAsync<Pessoa>(id);
        }
        public async Task<int> Insert(Pessoa pessoa)
        {
            var id = await _connection.InsertAsync(pessoa);
            return (int)id;
        }
        public async Task<bool> Update(Pessoa pessoa)
        {
            return await _connection.UpdateAsync(pessoa);
        }
        public async Task<bool> Delete(Pessoa pessoa)
        {
            return await _connection.DeleteAsync(pessoa);
        }
    }
}
