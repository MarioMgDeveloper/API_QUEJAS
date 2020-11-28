using System;
using System.Collections.Generic;

namespace API_QUEJAS.Models
{
    public partial class TbDepartamento
    {
        public TbDepartamento()
        {
            TbMunicipio = new HashSet<TbMunicipio>();
        }

        public int IdDepartamento { get; set; }
        public string CodigoDepartamento { get; set; }
        public int IdRegion { get; set; }
        public string NombreDepartamento { get; set; }

        public virtual TbRegion IdRegionNavigation { get; set; }
        public virtual ICollection<TbMunicipio> TbMunicipio { get; set; }
    }
}
