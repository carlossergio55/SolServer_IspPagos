using Aplicacion.DTOs.Pagos;
using Aplicacion.Interfaces.Repositories;
using Aplicacion.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Features.Pagos.Queries
{
    public class GetAllPendingPaymentsQuery : IRequest<Response<List<PagoPendienteDto>>>
    {
        // No se requieren parámetros para este query
    }

    public class GetAllPendingPaymentsQueryHandler : IRequestHandler<GetAllPendingPaymentsQuery, Response<List<PagoPendienteDto>>>
    {
        private readonly IClienteRepository _repo;

        public GetAllPendingPaymentsQueryHandler(IClienteRepository repo)
        {
            _repo = repo;
        }

        public async Task<Response<List<PagoPendienteDto>>> Handle(GetAllPendingPaymentsQuery request, CancellationToken cancellationToken)
        {
            var result = new Response<List<PagoPendienteDto>>();

            try
            {
                result = await _repo.ObtenerPagosPendientes();
                result.Message = result.Data.Count > 0 ? $"Query successful, {result.Data.Count} pending payments found." : "No pending payments found.";
                result.Succeeded = result.Data.Count > 0;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Succeeded = false;
            }

            return result;
        }
    }
}
