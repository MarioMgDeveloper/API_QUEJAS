using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_QUEJAS.ModelosPersonalizados
{
    public class ClsFiltroBusquedaComercio
    {
        public int IdDepto { get; set; }
        public int IdMuni { get; set; }
        public string Nombre { get; set; }
        public string Nit { get; set; }
        public int? IdEstado { get; set; }
    }
}
