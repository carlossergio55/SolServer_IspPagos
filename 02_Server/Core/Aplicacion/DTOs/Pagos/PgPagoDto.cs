using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs.Pagos
{
    public class PgPagoDto
    {
        public int IdpgPago { get; set; }
        public int IdpgCliente { get; set; }
        public DateTime MesPago { get; set; }
        public DateTime? FechaPago { get; set; }
        public decimal Monto { get; set; }
        public string EstadoPago { get; set; }

        public string Tipopago { get; set; }

        public PgClienteDto PgCliente { get; set; }
    }

}
