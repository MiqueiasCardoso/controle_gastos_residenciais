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



        [HttpGet("Transacao/ResumoFinanceiroPorPessoa")]
        public async Task<IActionResult> GetReumoFinanceiroPorPessoa()
        {
            return await ControllerHelper.ExecuteSafelyAsync(async () =>
            {
                List<ResumoFinanceiroPessoaViewModel> transacaos = await _transacaoService.BuscarResumoFinanceiroPorPessoa();
                return Ok(new ResultViewModel<List<ResumoFinanceiroPessoaViewModel>>(success: true, data: transacaos));
            });
        }
        
        [HttpGet("Transacao/ResumoFinanceiroPorCategoria")]
        public async Task<IActionResult> GetReumoFinanceiroPorCategoria()
        {
            return await ControllerHelper.ExecuteSafelyAsync(async () =>
            {
                List<ResumoFinanceiroCategoriaViewModel> transacaos = await _transacaoService.BuscarResumoFinanceiroPorCategoria();
                return Ok(new ResultViewModel<List<ResumoFinanceiroCategoriaViewModel>>(success: true, data: transacaos));
            });
        }
    }
}
