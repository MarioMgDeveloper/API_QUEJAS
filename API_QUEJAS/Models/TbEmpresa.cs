using System;
using System.Collections.Generic;

namespace API_QUEJAS.Models
{
    public partial class TbEmpresa
    {
        public TbEmpresa()
        {
            TbEstablecimiento = new HashSet<TbEstablecimiento>();
        }

        public int IdEmpresa { get; set; }
        public string NombreEmpresa { get; set; }
        public string DireccionFiscal { get; set; }
        public string Nit { get; set; }
        public int? IdEstado { get; set; }

        public virtual ICollection<TbEstablecimiento> TbEstablecimiento { get; set; }
    }
}
