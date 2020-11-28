using System;
using System.Collections.Generic;

namespace API_QUEJAS.Models
{
    public partial class TbEstablecimiento
    {
        public TbEstablecimiento()
        {
            TbQueja = new HashSet<TbQueja>();
        }

        public int IdEstablecimiento { get; set; }
        public int IdEmpresa { get; set; }
        public string NombreComplementario { get; set; }
        public string Direccion { get; set; }
        public string PatenteComercio { get; set; }
        public int IdMunicipio { get; set; }
        public int IdEstado { get; set; }

        public virtual TbEmpresa IdEmpresaNavigation { get; set; }
        public virtual TbEstado IdEstadoNavigation { get; set; }
        public virtual ICollection<TbQueja> TbQueja { get; set; }
    }
}
