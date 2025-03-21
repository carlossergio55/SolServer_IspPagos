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
    public class GetAllPgEquipoClienteQuery : IRequest<Response<List<PgEquipoClienteDto>>> { }

    public class GetAllPgEquipoClienteQueryHandler : IRequestHandler<GetAllPgEquipoClienteQuery, Response<List<PgEquipoClienteDto>>>
    {
        private readonly IRepositoryAsync<PgEquipoCliente> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetAllPgEquipoClienteQueryHandler(IRepositoryAsync<PgEquipoCliente> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<List<PgEquipoClienteDto>>> Handle(GetAllPgEquipoClienteQuery request, CancellationToken cancellationToken)
        {
            var equipos = await _repositoryAsync.ListAsync(new PgEquipoClienteSpecification(), cancellationToken);
            var equiposDto = _mapper.Map<List<PgEquipoClienteDto>>(equipos);
            return new Response<List<PgEquipoClienteDto>>(equiposDto);
        }
    }

    public class PgEquipoClienteSpecification : Specification<PgEquipoCliente>
    {
        public PgEquipoClienteSpecification()
        {
            Query.Include(x => x.PgCliente);
        }
    }

}
