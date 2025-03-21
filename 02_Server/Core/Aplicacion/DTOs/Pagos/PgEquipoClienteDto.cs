using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs.Pagos
{
    public class PgEquipoClienteDto
    {
        public int IdpgEquipo { get; set; }
        public int IdpgCliente { get; set; }
        public string UsuarioAiros { get; set; }
        public string ContraseñaAiros { get; set; }
        public string IpAiros { get; set; }
        public string MacAiros { get; set; }
        public string UsuarioRouter { get; set; }
        public string ContraseñaRouter { get; set; }
        public string IpRouter { get; set; }
        public string MacRouter { get; set; }
        public string NombreWifi { get; set; }
        public string ContraseñaWifi { get; set; }
        public string UsuarioWifi { get; set; }
        public string ContraseñaLoginWifi { get; set; }

        public PgClienteDto PgCliente { get; set; }
    }

}
