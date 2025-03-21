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
    public class UpdatePgCorteServicioCommand : IRequest<Response<int>>
    {
        public int IdpgCorte { get; set; }
        public DateTime FechaInicioCorte { get; set; }
        public DateTime? FechaFinCorte { get; set; }
        public string Motivo { get; set; }

        public class Handler : IRequestHandler<UpdatePgCorteServicioCommand, Response<int>>
        {
            private readonly IRepositoryAsync<PgCorteServicio> _repositoryAsync;

            public Handler(IRepositoryAsync<PgCorteServicio> repositoryAsync)
            {
                _repositoryAsync = repositoryAsync;
            }

            public async Task<Response<int>> Handle(UpdatePgCorteServicioCommand request, CancellationToken cancellationToken)
            {
                var entity = await _repositoryAsync.GetByIdAsync(request.IdpgCorte);
                if (entity == null)
                    throw new KeyNotFoundException("Registro no encontrado");

                entity.FechaInicioCorte = request.FechaInicioCorte;
                entity.FechaFinCorte = request.FechaFinCorte;
                entity.Motivo = request.Motivo;

                await _repositoryAsync.UpdateAsync(entity);
                return new Response<int>(entity.IdpgCorte);
            }
        }
    }

}
