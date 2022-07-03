using DigitalBrasilCash.Domain.Accounts.Input;
using DigitalBrasilCash.Domain.Accounts.Output;
using DigitalBrasilCash.Domain.Command;
using DigitalBrasilCash.Domain.Contracts.Command;
using DigitalBrasilCash.Domain.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DigitalBrasilCash.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    public class AccountController : Controller
    {
        private readonly IAccountWriteService _serviceWrite;

        public AccountController(IAccountWriteService serviceWrite)
        {
            _serviceWrite = serviceWrite;
        }

        [HttpPost("account")]
        [ProducesResponseType(typeof(AccountOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Novo([FromBody] AccountInput input)
        {
            try
            {
                ICommandResult retorno = await _serviceWrite.Novo(input);

                if (retorno.Sucesso)
                    return Ok(retorno);
                else if (!retorno.Sucesso && retorno.Dados != null)
                    return UnprocessableEntity(retorno);
                else
                    return BadRequest(retorno);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new CommandResult(false, ex.Message + ex.StackTrace));
            }
        }        
    }
}
