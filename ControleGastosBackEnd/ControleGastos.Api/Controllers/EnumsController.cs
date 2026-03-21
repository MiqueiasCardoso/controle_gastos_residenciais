using ControleGastos.Core.CoreViewModels;
using ControleGastos.Core.Enums;
using ControleGastos.Core.Models;
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
                       .Select(x => new EnumItem
                       {
                           Id = (int)x,
                           Nome = x.ToString()
                       })
                       .ToList();

            return Ok(new ResultViewModel<List<EnumItem>>(success: true, data: values));
        }

        [HttpGet("tipo-transacao")]
        public IActionResult GetTipoTransacao()
        {
            var values = Enum.GetValues(typeof(TipoTransacao))
                       .Cast<FinalidadeCategoria>()
                       .Select(x => new EnumItem
                       {
                           Id = (int)x,
                           Nome = x.ToString()
                       })
                       .ToList();

            return Ok(new ResultViewModel<List<EnumItem>>(success: true, data: values));
        }
    }
}
