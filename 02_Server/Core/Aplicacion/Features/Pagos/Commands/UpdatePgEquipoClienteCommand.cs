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
    public class UpdatePgEquipoClienteCommand : IRequest<Response<int>>
    {
        public int IdpgEquipo { get; set; }
        public int IdpgCliente { get; set; }
        public string UsuarioAiros { get; set; }
        public string ContraseñaAiros { get; set; }
        public string IpAiros { get; set; }
        public string MacAiros { get; set; }
        public string UsuarioRouter { get; set; }
        public string ContraseñaRouter { get; set; }
        public string IpRouter { get; set; }
        public string MacRouter { get; set; }
        public string NombreWifi { get; set; }
        public string ContraseñaWifi { get; set; }
        public string UsuarioWifi { get; set; }
        public string ContraseñaLoginWifi { get; set; }

        public class Handler : IRequestHandler<UpdatePgEquipoClienteCommand, Response<int>>
        {
            private readonly IRepositoryAsync<PgEquipoCliente> _repositoryAsync;

            public Handler(IRepositoryAsync<PgEquipoCliente> repositoryAsync)
            {
                _repositoryAsync = repositoryAsync;
            }

            public async Task<Response<int>> Handle(UpdatePgEquipoClienteCommand request, CancellationToken cancellationToken)
            {
                var entity = await _repositoryAsync.GetByIdAsync(request.IdpgEquipo);
                if (entity == null)
                    throw new KeyNotFoundException("Equipo no encontrado");

                entity.IdpgCliente = request.IdpgCliente;
                entity.UsuarioAiros = request.UsuarioAiros;
                entity.ContraseñaAiros = request.ContraseñaAiros;
                entity.IpAiros = request.IpAiros;
                entity.MacAiros = request.MacAiros;
                entity.UsuarioRouter = request.UsuarioRouter;
                entity.ContraseñaRouter = request.ContraseñaRouter;
                entity.IpRouter = request.IpRouter;
                entity.MacRouter = request.MacRouter;
                entity.NombreWifi = request.NombreWifi;
                entity.ContraseñaWifi = request.ContraseñaWifi;
                entity.ContraseñaWifi = request.ContraseñaWifi;
                entity.UsuarioWifi = request.UsuarioWifi;
                entity.ContraseñaLoginWifi = request.ContraseñaLoginWifi;

                await _repositoryAsync.UpdateAsync(entity);
                return new Response<int>(entity.IdpgEquipo);
            }
        }
    }


}
