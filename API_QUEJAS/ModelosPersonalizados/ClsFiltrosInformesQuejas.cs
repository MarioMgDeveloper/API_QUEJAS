using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_QUEJAS.ModelosPersonalizados
{
    public class ClsFiltrosInformesQuejas
    {
        public int? IdRegion { get; set; }
        public int? IdDepartamento { get; set; }
        public int? IdMunicipio { get; set; }
        public int? IdEstado { get; set; }
        public string Nombrecomercio { get; set; }
        public DateTime? Del { get; set; }
        public DateTime? Al { get; set; }
    }
}
