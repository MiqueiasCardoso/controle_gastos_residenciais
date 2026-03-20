using ControleGastos.Api.Helpers;
using ControleGastos.Api.Services;
using ControleGastos.Core.CoreViewModels;
using ControleGastos.Core.Enums;
using ControleGastos.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers
{
    [ApiController]
    [Route("v1")]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaService _categoriaService;
        public CategoriaController(CategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet("Categoria/")]
        public async Task<IActionResult> Get()
        {
            return await ControllerHelper.ExecuteSafelyAsync(async () =>
            {
                List<Categoria> categorias = await _categoriaService.BuscarCategorias();
                return Ok(new ResultViewModel<List<Categoria>>(success: true, data: categorias));
            });
        }

        [HttpPost("Categoria/")]
        public async Task<IActionResult> Post([FromBody] Categoria categoriaBody)
        {
            return await ControllerHelper.ExecuteSafelyAsync(async () =>
            {
                if (!Enum.IsDefined(typeof(FinalidadeCategoria), categoriaBody.Finalidade))
                {
                    return BadRequest(
                        new ResultViewModel<string>(
                            success: false,
                            data: null,
                            errors: new List<string> { "Finalidade Inválida" }
                        )
                    );
                }

                await _categoriaService.InserirCategoria(categoriaBody);
                return Ok(new ResultViewModel<Categoria>(true, categoriaBody));
            });
        }
    }
}
