using System;
using System.Collections.Generic;

namespace API_QUEJAS.Models
{
    public partial class TbRol
    {
        public TbRol()
        {
            TbUsuario = new HashSet<TbUsuario>();
        }

        public int IdRol { get; set; }
        public string NombreRol { get; set; }

        public virtual ICollection<TbUsuario> TbUsuario { get; set; }
    }
}
