using Aplicacion.DTOs.Clasificador;
using Aplicacion.DTOs.Pagos;
using Aplicacion.DTOs.Segurity;
using AutoMapper;
using Dominio.Entities;
using Dominio.Entities.Pagos;
using Dominio.Entities.Seguridad;



namespace Aplicacion.Mappings
{
    public class GeneralMappings : Profile
    {

        public GeneralMappings()
        {
            //TODO: Agregar aqui el registro de mapeo para obtenion de consultas  direccion  EntidadDominio --> ModeloDto
            #region QueryDto
            CreateMap<SegvUsuario, SegUsuarioDto>();
            CreateMap<SegUsuario, SeUsuarioDto>();
            CreateMap<SegvMenuobjetos, UserMenuDto>();


            CreateMap<GenClasificador, GenClasificadorDto>();
            CreateMap<GenClasificadortipo, GenClasificadortipoDto>();
            CreateMap<PgCliente, PgClienteDto>();
            CreateMap<PgCorteServicio, PgCorteServicioDto>();
            CreateMap<PgPago, PgPagoDto>();
            CreateMap<PgPago, PagoPendienteDto>();
            CreateMap<PgEquipoCliente, PgEquipoClienteDto>();

            #endregion
            //TODO: Agregar aqui el registro de mapeo para ejecucion de comandos  direccion  ModeloDto --> EntidadDominio Ej. : CreateMap<ProductoDto, CapProducto>();

            #region Commands
            CreateMap<SeUsuarioDto, SegUsuario>();
            //CreateMap<ConInventarioDto, ConInventario>();
            CreateMap<GenClasificadorDto, GenClasificador>();
            CreateMap<GenClasificadortipoDto, GenClasificadortipo>();
            CreateMap<PgClienteDto, PgCliente>();
            CreateMap<PgCorteServicioDto, PgCorteServicio>();
            CreateMap<PgPagoDto, PgPago>();
            CreateMap<PgEquipoClienteDto, PgEquipoCliente>();

            #endregion
        }
    }
}
