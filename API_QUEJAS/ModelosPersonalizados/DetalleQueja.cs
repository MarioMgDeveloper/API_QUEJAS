using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_QUEJAS.ModelosPersonalizados
{
    public class DetalleQueja
    {
        public string EstadoQueja { get; set; }
        public string Queja { get; set; }
        public string Ubicacion { get; set; }
        public string Comercio { get; set; }
        public string Direccion { get; set; }
        public string Resolucion { get; set; }
    }
}
