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
    public class CreatePgClienteCommand : IRequest<Response<int>>
    {
        public PgClienteDto _PgCliente { get; set; }

        public class Handler : IRequestHandler<CreatePgClienteCommand, Response<int>>
        {
            private readonly IRepositoryAsync<PgCliente> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<PgCliente> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<int>> Handle(CreatePgClienteCommand request, CancellationToken cancellationToken)
            {
                var entity = _mapper.Map<PgCliente>(request._PgCliente);
                var newRecord = await _repositoryAsync.AddAsync(entity);
                return new Response<int>(newRecord.IdpgCliente);
            }
        }
    }

}
