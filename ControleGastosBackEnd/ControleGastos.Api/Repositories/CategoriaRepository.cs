using ControleGastos.Core.Models;
using Dapper.Contrib.Extensions;
using System.Data;

namespace ControleGastos.Api.Repositories
{
    public class CategoriaRepository
    {
        private readonly IDbConnection _connection;

        public CategoriaRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<List<Categoria>> Get()
        {
            var categorias = await _connection.GetAllAsync<Categoria>();
            return categorias.ToList();
        }
        public async Task<Categoria> GetById(int id)
        {
            return await _connection.GetAsync<Categoria>(id);
        }
        public async Task Insert(Categoria categoria)
        {
            await _connection.InsertAsync(categoria);
        }
    }
}
