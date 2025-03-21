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
    public class CreatePgPagoCommand : IRequest<Response<int>>
    {
        public PgPagoDto _PgPago { get; set; }
        public class Handler : IRequestHandler<CreatePgPagoCommand, Response<int>>
        {
            private readonly IRepositoryAsync<PgPago> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<PgPago> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<int>> Handle(CreatePgPagoCommand request, CancellationToken cancellationToken)
            {
                var entity = _mapper.Map<PgPago>(request._PgPago);
                var newRecord = await _repositoryAsync.AddAsync(entity);
                return new Response<int>(newRecord.IdpgPago);
            }
        }
    }

}
