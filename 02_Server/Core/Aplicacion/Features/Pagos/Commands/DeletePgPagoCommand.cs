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
    public class DeletePgPagoCommand : IRequest<Response<int>>
    {
        public int IdpgPago { get; set; }

        public class Handler : IRequestHandler<DeletePgPagoCommand, Response<int>>
        {
            private readonly IRepositoryAsync<PgPago> _repositoryAsync;

            public Handler(IRepositoryAsync<PgPago> repositoryAsync)
            {
                _repositoryAsync = repositoryAsync;
            }

            public async Task<Response<int>> Handle(DeletePgPagoCommand request, CancellationToken cancellationToken)
            {
                var entity = await _repositoryAsync.GetByIdAsync(request.IdpgPago);
                if (entity == null)
                    throw new KeyNotFoundException("Pago no encontrado");

                await _repositoryAsync.DeleteAsync(entity);
                return new Response<int>(entity.IdpgPago);
            }
        }
    }

}
