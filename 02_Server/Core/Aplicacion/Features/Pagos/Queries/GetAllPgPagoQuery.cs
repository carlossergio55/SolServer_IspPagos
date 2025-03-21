using Aplicacion.DTOs.Pagos;
using Aplicacion.Interfaces;
using Aplicacion.Wrappers;
using Ardalis.Specification;
using AutoMapper;
using Dominio.Entities.Pagos;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Features.Pagos.Queries
{
    public class GetAllPgPagoQuery : IRequest<Response<List<PgPagoDto>>> { }

    public class GetAllPgPagoQueryHandler : IRequestHandler<GetAllPgPagoQuery, Response<List<PgPagoDto>>>
    {
        private readonly IRepositoryAsync<PgPago> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetAllPgPagoQueryHandler(IRepositoryAsync<PgPago> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<List<PgPagoDto>>> Handle(GetAllPgPagoQuery request, CancellationToken cancellationToken)
        {
            var pagos = await _repositoryAsync.ListAsync(new PgPagoSpecification(), cancellationToken);
            var pagosDto = _mapper.Map<List<PgPagoDto>>(pagos);
            return new Response<List<PgPagoDto>>(pagosDto);
        }
    }

    public class PgPagoSpecification : Specification<PgPago>
    {
        public PgPagoSpecification()
        {
            Query.Include(x => x.PgCliente);
        }
    }

}
