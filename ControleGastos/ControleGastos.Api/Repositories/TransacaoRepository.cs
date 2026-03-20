using ControleGastos.Core.Models;
using Dapper.Contrib.Extensions;
using System.Data;

namespace ControleGastos.Api.Repositories
{
    public class TransacaoRepository
    {
        private readonly IDbConnection _connection;

        public TransacaoRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<List<Transacao>> Get()
        {
            var transacoes = await _connection.GetAllAsync<Transacao>();
            return transacoes.ToList();
        }
        public async Task<Transacao> GetById(int id)
        {
            return await _connection.GetAsync<Transacao>(id);
        }
        public async Task Insert(Transacao transacao)
        {
            await _connection.InsertAsync(transacao);
        }
    }
}
