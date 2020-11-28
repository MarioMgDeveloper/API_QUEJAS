using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_QUEJAS.ModelosPersonalizados
{
    public class ClsInfoSucursales
    {
        public int IdSucursal { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }
        public string Direccion { get; set; }
        public int? IdDepartamento { get; set; }
        public int? IdMunicipio { get; set; }
        public int? IdEstado { get; set; }
        public string Patente { get; set; }
    }
}
