using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs.Pagos
{
    public class PagoPendienteDto
    {
        public int ClienteId { get; set; }
        public string NombreCliente { get; set; }
        public string TelefonoCliente { get; set; }
        public string MesDeuda { get; set; }
        public decimal Monto { get; set; }
        public string EstadoPago { get; set; }
    }

}
