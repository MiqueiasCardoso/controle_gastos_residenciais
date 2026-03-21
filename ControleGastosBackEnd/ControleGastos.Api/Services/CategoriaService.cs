using ControleGastos.Api.Repositories;
using ControleGastos.Core.Models;

namespace ControleGastos.Api.Services
{
    public class CategoriaService
    {
        private readonly CategoriaRepository _categoriaRepository;

        public CategoriaService(CategoriaRepository CctegoriaSerRepository)
        {
            _categoriaRepository = CctegoriaSerRepository;
        }
        public async Task<List<Categoria>> BuscarCategorias()
        {
            var categorias = await _categoriaRepository.Get();
            return categorias;
        }
        public async Task<Categoria> BuscarCategoriaPorId(int id)
        {
            var categoria = await _categoriaRepository.GetById(id);
            return categoria;
        }
        public async Task<Categoria> InserirCategoria(Categoria categoria)
        {
            await _categoriaRepository.Insert(categoria);
            return categoria;
        }

    }
}
