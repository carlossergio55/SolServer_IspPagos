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
    [Table("pg_equipo_cliente", Schema = "public")]
    public partial class PgEquipoCliente : AuditableBaseEntity
    {
        [Key]
        public int IdpgEquipo { get; set; }

        [ForeignKey("PgCliente")]
        public int IdpgCliente { get; set; }

        public string UsuarioAiros { get; set; } = "ubnt";
        public string ContraseñaAiros { get; set; }
        public string IpAiros { get; set; }
        public string MacAiros { get; set; }
        public string UsuarioRouter { get; set; } = "admin";
        public string ContraseñaRouter { get; set; }
        public string IpRouter { get; set; }
        public string MacRouter { get; set; }
        public string NombreWifi { get; set; }
        public string ContraseñaWifi { get; set; }
        public string UsuarioWifi { get; set; }
        public string ContraseñaLoginWifi { get; set; }

        public virtual PgCliente PgCliente { get; set; }
    }

}
