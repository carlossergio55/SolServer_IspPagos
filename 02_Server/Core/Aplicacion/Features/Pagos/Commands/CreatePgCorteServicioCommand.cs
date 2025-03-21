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

namespace Aplicacion.Features.Pagos.Commands
{
    public class CreatePgCorteServicioCommand : IRequest<Response<int>>
    {
        public PgCorteServicioDto _PgCorteServicio { get; set; }

        public class Handler : IRequestHandler<CreatePgCorteServicioCommand, Response<int>>
        {
            private readonly IRepositoryAsync<PgCorteServicio> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<PgCorteServicio> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<int>> Handle(CreatePgCorteServicioCommand request, CancellationToken cancellationToken)
            {
                var entity = _mapper.Map<PgCorteServicio>(request._PgCorteServicio);
                var newRecord = await _repositoryAsync.AddAsync(entity);
                return new Response<int>(newRecord.IdpgCorte);
            }
        }
    }

}
