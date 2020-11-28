using System;
using System.Collections.Generic;

namespace API_QUEJAS.Models
{
    public partial class TbRegion
    {
        public TbRegion()
        {
            TbDepartamento = new HashSet<TbDepartamento>();
        }

        public int IdRegion { get; set; }
        public string NombrRegion { get; set; }

        public virtual ICollection<TbDepartamento> TbDepartamento { get; set; }
    }
}
