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
    public class DeletePgCorteServicioCommand : IRequest<Response<int>>
    {
        public int IdpgCorte { get; set; }

        public class Handler : IRequestHandler<DeletePgCorteServicioCommand, Response<int>>
        {
            private readonly IRepositoryAsync<PgCorteServicio> _repositoryAsync;

            public Handler(IRepositoryAsync<PgCorteServicio> repositoryAsync)
            {
                _repositoryAsync = repositoryAsync;
            }

            public async Task<Response<int>> Handle(DeletePgCorteServicioCommand request, CancellationToken cancellationToken)
            {
                var entity = await _repositoryAsync.GetByIdAsync(request.IdpgCorte);
                if (entity == null)
                    throw new KeyNotFoundException("Registro no encontrado");

                await _repositoryAsync.DeleteAsync(entity);
                return new Response<int>(entity.IdpgCorte);
            }
        }
    }

}
