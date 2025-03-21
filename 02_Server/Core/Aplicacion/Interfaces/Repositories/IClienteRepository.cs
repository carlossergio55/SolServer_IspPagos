using Aplicacion.DTOs.Pagos;
using Aplicacion.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces.Repositories
{
    public interface IClienteRepository
    {
        Task<Response<List<PagoPendienteDto>>> ObtenerPagosPendientes();
    }
}
