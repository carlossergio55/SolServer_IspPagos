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
    public class DeletePgEquipoClienteCommand : IRequest<Response<int>>
    {
        public int IdpgEquipo { get; set; }

        public class Handler : IRequestHandler<DeletePgEquipoClienteCommand, Response<int>>
        {
            private readonly IRepositoryAsync<PgEquipoCliente> _repositoryAsync;

            public Handler(IRepositoryAsync<PgEquipoCliente> repositoryAsync)
            {
                _repositoryAsync = repositoryAsync;
            }

            public async Task<Response<int>> Handle(DeletePgEquipoClienteCommand request, CancellationToken cancellationToken)
            {
                var entity = await _repositoryAsync.GetByIdAsync(request.IdpgEquipo);
                if (entity == null)
                    throw new KeyNotFoundException("Equipo no encontrado");

                await _repositoryAsync.DeleteAsync(entity);
                return new Response<int>(entity.IdpgEquipo);
            }
        }
    }

}
