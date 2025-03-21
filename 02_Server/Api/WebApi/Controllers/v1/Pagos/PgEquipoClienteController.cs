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
    public class PgEquipoClienteController : BaseApiController
    {
        [HttpGet("GetAll")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllPgEquipoClienteQuery()));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CreatePgEquipoClienteCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, UpdatePgEquipoClienteCommand command)
        {
            if (id != command.IdpgEquipo)
                return BadRequest();

            return Ok(await Mediator.Send(command));
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeletePgEquipoClienteCommand { IdpgEquipo = id }));
        }

    }
}
