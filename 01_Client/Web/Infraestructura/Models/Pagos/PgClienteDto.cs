using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Models.Pagos
{
    public class PgClienteDto
    {
        public int IdpgCliente { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public string EstadoServicio { get; set; }
    }
}
