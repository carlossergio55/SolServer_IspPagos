using Dominio.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entities.Pagos
{
    [Table("pg_pago", Schema = "public")]
    public partial class PgPago : AuditableBaseEntity
    {
        [Key]
        public int IdpgPago { get; set; }

        [ForeignKey("PgCliente")]
        public int IdpgCliente { get; set; }

        public DateTime MesPago { get; set; }
        public DateTime? FechaPago { get; set; }
        public decimal Monto { get; set; }
        public string EstadoPago { get; set; } = "Pendiente";
        public string Tipopago { get; set; }
        public bool Recibido { get; set; }
        public virtual PgCliente PgCliente { get; set; }
    }

}
