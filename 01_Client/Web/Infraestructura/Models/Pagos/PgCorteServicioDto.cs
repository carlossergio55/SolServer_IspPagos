using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Models.Pagos
{
    public class PgCorteServicioDto
    {
        public int IdpgCorte { get; set; }
        public DateTime? FechaInicioCorte { get; set; }
        public DateTime? FechaFinCorte { get; set; }
        public string Motivo { get; set; }
        public int IdpgCliente { get; set; }
        public string NombreCliente { get; set; }
    }
}
