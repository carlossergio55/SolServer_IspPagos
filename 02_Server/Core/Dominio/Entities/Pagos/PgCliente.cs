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
    [Table("pg_cliente", Schema = "public")]
    public partial class PgCliente 
    {
        [Key]
        public int IdpgCliente { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaInicio { get; set; }
        public string EstadoServicio { get; set; } = "Activo";
        public virtual ICollection<PgPago> PgPagos { get; set; }

    }

}
