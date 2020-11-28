using System;
using System.Collections.Generic;

namespace API_QUEJAS.Models
{
    public partial class TbUsuario
    {
        public TbUsuario()
        {
            TbQueja = new HashSet<TbQueja>();
        }

        public int IdUsuario { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public int IdEstado { get; set; }
        public int IdRol { get; set; }
        public int IdPersona { get; set; }

        public virtual TbEstado IdEstadoNavigation { get; set; }
        public virtual TbPersona IdPersonaNavigation { get; set; }
        public virtual TbRol IdRolNavigation { get; set; }
        public virtual ICollection<TbQueja> TbQueja { get; set; }
    }
}
