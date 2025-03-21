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
    public class GetAllPgCorteServicioQuery : IRequest<Response<List<PgCorteServicioDto>>> { }

    public class GetAllPgCorteServicioQueryHandler : IRequestHandler<GetAllPgCorteServicioQuery, Response<List<PgCorteServicioDto>>>
    {
        private readonly IRepositoryAsync<PgCorteServicio> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetAllPgCorteServicioQueryHandler(IRepositoryAsync<PgCorteServicio> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<List<PgCorteServicioDto>>> Handle(GetAllPgCorteServicioQuery request, CancellationToken cancellationToken)
        {
            var cortes = await _repositoryAsync.ListAsync(cancellationToken);
            var cortesDto = _mapper.Map<List<PgCorteServicioDto>>(cortes);
            return new Response<List<PgCorteServicioDto>>(cortesDto);
        }
    }

}
