using ControleGastos.Api.Exceptions;
using ControleGastos.Core.CoreViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;

namespace ControleGastos.Api.Helpers
{
    public static class ControllerHelper
    {
        public static async Task<IActionResult> ExecuteSafelyAsync(Func<Task<IActionResult>> action)
        {
            try
            {
                return await action();
            }
            catch (NotFoundException ex)
            {
                return new NotFoundObjectResult(
                    new ResultViewModel<string>(success: false, data: null, errors: new() { ex.Message })
                );
            }
            catch (BusinessException ex)
            {
                return new NotFoundObjectResult(
                    new ResultViewModel<string>(false, ex.Message)
                );
            }
            catch (DbException ex)
            {
                return new ObjectResult(
                    new ResultViewModel<string>(
                        success: false,
                        data: null,
                        errors: new() { $"Erro ao acessar o banco de dados.\n{ex.Message}" }
                    )
                )
                {
                    StatusCode = 500
                };
            }
            catch (ValidationException ex)
            {
                return new BadRequestObjectResult(
                    new ResultViewModel<string>(success: false, data: null, errors: new() { ex.Message })
                );
            }
            catch (Exception ex)
            {
                return new ObjectResult(
                    new ResultViewModel<string>(success: false, data: null, errors: new() { ex.Message })

                )
                {
                    StatusCode = 500
                };
            }
        }
    }
}
