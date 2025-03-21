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
    public class DeletePgClienteCommand : IRequest<Response<int>>
    {
        public int IdpgCliente { get; set; }

        public class Handler : IRequestHandler<DeletePgClienteCommand, Response<int>>
        {
            private readonly IRepositoryAsync<PgCliente> _repositoryAsync;

            public Handler(IRepositoryAsync<PgCliente> repositoryAsync)
            {
                _repositoryAsync = repositoryAsync;
            }

            public async Task<Response<int>> Handle(DeletePgClienteCommand request, CancellationToken cancellationToken)
            {
                var entity = await _repositoryAsync.GetByIdAsync(request.IdpgCliente);
                if (entity == null)
                    throw new KeyNotFoundException("Cliente no encontrado");

                await _repositoryAsync.DeleteAsync(entity);
                return new Response<int>(entity.IdpgCliente);
            }
        }
    }

}
