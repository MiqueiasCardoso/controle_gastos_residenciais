using ControleGastos.Api.Services;
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
        public async Task<IActionResult> GetById()
        {
            return Ok();
        }

        [HttpPost("Pessoa/")]
        public async Task<IActionResult> Post([FromBody] Pessoa pessoaBody)
        {
            return Ok();
        }

        [HttpPut("Pessoa/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Pessoa pessoaBody)
        {
            return Ok();
        }
        [HttpDelete("Pessoa/{id}")]
        public async Task<IActionResult> Delete(int id, [FromBody] Pessoa pessoa)
        {
            return Ok();
        }

    }
}
