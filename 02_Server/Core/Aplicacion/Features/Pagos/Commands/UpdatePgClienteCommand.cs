using Aplicacion.Interfaces;
using Aplicacion.Wrappers;
using Dominio.Entities.Pagos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Features.Pagos.Commands
{
    public class UpdatePgClienteCommand : IRequest<Response<int>>
    {
        public int IdpgCliente { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaInicio { get; set; }
        public string EstadoServicio { get; set; }

        public class Handler : IRequestHandler<UpdatePgClienteCommand, Response<int>>
        {
            private readonly IRepositoryAsync<PgCliente> _repositoryAsync;

            public Handler(IRepositoryAsync<PgCliente> repositoryAsync)
            {
                _repositoryAsync = repositoryAsync;
            }

            public async Task<Response<int>> Handle(UpdatePgClienteCommand request, CancellationToken cancellationToken)
            {
                var entity = await _repositoryAsync.GetByIdAsync(request.IdpgCliente);
                if (entity == null)
                    throw new KeyNotFoundException("Cliente no encontrado");

                entity.Nombre = request.Nombre;
                entity.Telefono = request.Telefono;
                entity.Direccion = request.Direccion;
                entity.FechaInicio = request.FechaInicio;
                entity.EstadoServicio = request.EstadoServicio;

                await _repositoryAsync.UpdateAsync(entity);
                return new Response<int>(entity.IdpgCliente);
            }
        }
    }

}
