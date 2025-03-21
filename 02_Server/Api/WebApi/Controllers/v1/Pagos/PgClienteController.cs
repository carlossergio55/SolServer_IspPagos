using Aplicacion.Features.Pagos.Commands;
using Aplicacion.Features.Pagos.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Webapi.Controllers.v1;

namespace WebApi.Controllers.v1.Pagos
{
    [ApiVersion("1.0")]
    [ApiController]
    public class PgClienteController : BaseApiController
    {
        [HttpGet("GetAll")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllPgClientesQuery()));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CreatePgClienteCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, UpdatePgClienteCommand command)
        {
            if (id != command.IdpgCliente)
                return BadRequest();

            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeletePgClienteCommand { IdpgCliente = id }));
        }
    }
}
