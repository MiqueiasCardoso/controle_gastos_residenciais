using ControleGastos.Api.Exceptions;
using ControleGastos.Api.Helpers;
using ControleGastos.Api.Services;
using ControleGastos.Core.CoreViewModels;
using ControleGastos.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers
{
    [ApiController]
    [Route("v1")]
    public class PessoaController : ControllerBase
    {
        private readonly PessoaService _pessoaService;

        public PessoaController(PessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        [HttpGet("Pessoa/")]
        public async Task<IActionResult> Get()
        {
            return await ControllerHelper.ExecuteSafelyAsync(async () =>
            {
                List<Pessoa> pessoas = await _pessoaService.BuscarPessoas();
                return Ok(new ResultViewModel<List<Pessoa>>(success: true, data: pessoas));
            });
        }

        [HttpGet("Pessoa/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return await ControllerHelper.ExecuteSafelyAsync(async () =>
            {
                Pessoa pessoa = await _pessoaService.BuscarPessoaPorId(id);
                return Ok(new ResultViewModel<Pessoa>(success: true, data: pessoa));
            });
        }

        [HttpPost("Pessoa/")]
        public async Task<IActionResult> Post([FromBody] Pessoa pessoaBody)
        {
            return await ControllerHelper.ExecuteSafelyAsync(async () =>
            {
                await _pessoaService.InserirPessoa(pessoaBody);
                return Ok(new ResultViewModel<Pessoa>(true, pessoaBody));
            });
        }

        [HttpPut("Pessoa/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Pessoa pessoaBody)
        {
            return await ControllerHelper.ExecuteSafelyAsync(async () =>
            {
                if (id != pessoaBody.Id)
                {
                    return BadRequest(
                       new ResultViewModel<string>(
                           success: false,
                           data: null,
                           errors: new List<string> { "Identificação da pessoa na requisição diferente da Identificação no corpo dela." }
                       )
                   );
                }
                Pessoa pessoa = await _pessoaService.AtualizarPessoa(id, pessoaBody);
                return Ok(new ResultViewModel<Pessoa>(true, pessoa));
            });
        }

        [HttpDelete("Pessoa/{id}")]
        public async Task<IActionResult> Delete(int id, [FromBody] Pessoa pessoaBody)
        {
            return await ControllerHelper.ExecuteSafelyAsync(async () =>
            {
                if (id != pessoaBody.Id)
                {
                    return BadRequest(
                       new ResultViewModel<string>(
                           success: false,
                           data: null,
                           errors: new List<string> { "Identificação da pessoa na requisição diferente da Identificação no corpo dela." }
                       )
                   );
                }
                await _pessoaService.RemoverPessoa(id, pessoaBody);
                return Ok(new ResultViewModel<String>(true, ""));
            });
        }

    }
}
