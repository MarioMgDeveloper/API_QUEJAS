using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_QUEJAS.ModelosPersonalizados
{
    public class ClsInfoUsuario
    {
        public int IdUsuario { get; set; }
        public string Correo { get; set; }
        public string Nombre { get; set; }
        public int IdRol { get; set; }
        public string NombreRol { get; set; }
        public string JWT { get; set; }
    }
}
