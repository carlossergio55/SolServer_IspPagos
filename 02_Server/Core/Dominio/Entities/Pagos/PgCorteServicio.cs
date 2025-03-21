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
    [Table("pg_corte_servicio", Schema = "public")]
    public partial class PgCorteServicio : AuditableBaseEntity
    {
        [Key]
        public int IdpgCorte { get; set; }
        public DateTime FechaInicioCorte { get; set; }
        public DateTime? FechaFinCorte { get; set; }
        public string Motivo { get; set; }
        public int IdpgCliente { get; set; }
    }

}
