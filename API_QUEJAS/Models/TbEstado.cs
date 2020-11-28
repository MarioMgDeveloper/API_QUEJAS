using System;
using System.Collections.Generic;

namespace API_QUEJAS.Models
{
    public partial class TbEstado
    {
        public TbEstado()
        {
            TbEstablecimiento = new HashSet<TbEstablecimiento>();
            TbQueja = new HashSet<TbQueja>();
            TbUsuario = new HashSet<TbUsuario>();
        }

        public int IdEstado { get; set; }
        public string NombreEstado { get; set; }

        public virtual ICollection<TbEstablecimiento> TbEstablecimiento { get; set; }
        public virtual ICollection<TbQueja> TbQueja { get; set; }
        public virtual ICollection<TbUsuario> TbUsuario { get; set; }
    }
}
