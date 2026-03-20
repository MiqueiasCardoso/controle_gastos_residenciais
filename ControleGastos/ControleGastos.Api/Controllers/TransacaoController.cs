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
    public class TransacaoController : ControllerBase
    {
        private readonly TransacaoService _transacaoService;
        public TransacaoController(TransacaoService transacaoService)
        {
            _transacaoService = transacaoService;
        }

        [HttpGet("Transacao/")]
        public async Task<IActionResult> Get()
        {
            return await ControllerHelper.ExecuteSafelyAsync(async () =>
            {
                List<Transacao> transacaos = await _transacaoService.BuscarTransacaos();
                return Ok(new ResultViewModel<List<Transacao>>(success: true, data: transacaos));
            });
        }

        [HttpPost("Transacao/")]
        public async Task<IActionResult> Post([FromBody] Transacao transacaoBody)
        {
            return await ControllerHelper.ExecuteSafelyAsync(async () =>
            {
                if (!Enum.IsDefined(typeof(TipoTransacao), transacaoBody.Tipo))
                {
                    return BadRequest(
                        new ResultViewModel<string>(
                            success: false,
                            data: null,
                            errors: new List<string> { "Tipo Inválido" }
                        )
                    );
                }

                await _transacaoService.InserirTransacao(transacaoBody);
                return Ok(new ResultViewModel<Transacao>(true, transacaoBody));
            });
        }
    }
}
