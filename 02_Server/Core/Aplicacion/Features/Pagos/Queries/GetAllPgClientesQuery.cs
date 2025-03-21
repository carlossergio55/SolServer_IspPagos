using Aplicacion.DTOs.Pagos;
using Aplicacion.Interfaces;
using Aplicacion.Wrappers;
using AutoMapper;
using Dominio.Entities.Pagos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Features.Pagos.Queries
{
    public class GetAllPgClientesQuery : IRequest<Response<List<PgClienteDto>>> { }

    public class GetAllPgClientesQueryHandler : IRequestHandler<GetAllPgClientesQuery, Response<List<PgClienteDto>>>
    {
        private readonly IRepositoryAsync<PgCliente> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetAllPgClientesQueryHandler(IRepositoryAsync<PgCliente> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<List<PgClienteDto>>> Handle(GetAllPgClientesQuery request, CancellationToken cancellationToken)
        {
            var clientes = await _repositoryAsync.ListAsync(cancellationToken);
            var clientesDto = _mapper.Map<List<PgClienteDto>>(clientes);
            return new Response<List<PgClienteDto>>(clientesDto);
        }
    }

}
