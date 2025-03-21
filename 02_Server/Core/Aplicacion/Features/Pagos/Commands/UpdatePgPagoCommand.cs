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
    public class UpdatePgPagoCommand : IRequest<Response<int>>
    {
        public int IdpgPago { get; set; }
        public int IdpgCliente { get; set; }
        public DateTime MesPago { get; set; }
        public DateTime? FechaPago { get; set; }
        public decimal Monto { get; set; }
        public string Tipopago { get; set; }
        public string EstadoPago { get; set; }

        public class Handler : IRequestHandler<UpdatePgPagoCommand, Response<int>>
        {
            private readonly IRepositoryAsync<PgPago> _repositoryAsync;

            public Handler(IRepositoryAsync<PgPago> repositoryAsync)
            {
                _repositoryAsync = repositoryAsync;
            }

            public async Task<Response<int>> Handle(UpdatePgPagoCommand request, CancellationToken cancellationToken)
            {
                var entity = await _repositoryAsync.GetByIdAsync(request.IdpgPago);
                if (entity == null)
                    throw new KeyNotFoundException("Pago no encontrado");

                entity.IdpgCliente = request.IdpgCliente;
                entity.MesPago = request.MesPago;
                entity.FechaPago = request.FechaPago;
                entity.Monto = request.Monto;
                entity.Tipopago = request.Tipopago;
                entity.EstadoPago = request.EstadoPago;

                await _repositoryAsync.UpdateAsync(entity);
                return new Response<int>(entity.IdpgPago);
            }
        }
    }

}
