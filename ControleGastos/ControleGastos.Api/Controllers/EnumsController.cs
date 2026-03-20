using ControleGastos.Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers
{
    [ApiController]
    [Route("v1")]
    public class EnumsController : ControllerBase
    {
        [HttpGet("finalidade-categoria")]
        public IActionResult GetFinalidadeCategoria()
        {
            var values = Enum.GetValues(typeof(FinalidadeCategoria))
                .Cast<FinalidadeCategoria>()
                .Select(x => new
                {
                    Id = (int)x,
                    Nome = x.ToString()
                });

            return Ok(values);
        }

        [HttpGet("tipo-transacao")]
        public IActionResult GetTipoTransacao()
        {
            var values = Enum.GetValues(typeof(TipoTransacao))
                .Cast<FinalidadeCategoria>()
                .Select(x => new
                {
                    Id = (int)x,
                    Nome = x.ToString()
                });

            return Ok(values);
        }
    }
}
