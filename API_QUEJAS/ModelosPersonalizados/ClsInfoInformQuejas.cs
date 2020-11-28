using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_QUEJAS.ModelosPersonalizados
{
    public class ClsInfoInformQuejas
    {
        public string Empresa { get; set; }
        public string Region { get; set; }
        public string Ubicacion { get; set; }
        public string Direccion { get; set; }
        public int IdEstado { get; set; }
        public string Estado { get; set; }
        public string Queja { get; set; }
        public string Resolucion { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaModificacion { get; set; }

        public int IdQueja { get; set; }
        public string DescripcioResuelve { get; set; }
        public bool check { get; set; }
    }
}
