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
    public class CreatePgEquipoClienteCommand : IRequest<Response<int>>
    {
        public PgEquipoClienteDto _PgEquipoCliente { get; set; }

        public class Handler : IRequestHandler<CreatePgEquipoClienteCommand, Response<int>>
        {
            private readonly IRepositoryAsync<PgEquipoCliente> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<PgEquipoCliente> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<int>> Handle(CreatePgEquipoClienteCommand request, CancellationToken cancellationToken)
            {
                var entity = _mapper.Map<PgEquipoCliente>(request._PgEquipoCliente);
                var newRecord = await _repositoryAsync.AddAsync(entity);
                return new Response<int>(newRecord.IdpgEquipo);
            }
        }
    }

}
