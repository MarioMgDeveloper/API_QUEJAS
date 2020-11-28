using System;
using System.Collections.Generic;

namespace API_QUEJAS.Models
{
    public partial class TbPersona
    {
        public TbPersona()
        {
            TbUsuario = new HashSet<TbUsuario>();
        }

        public int IdPersona { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string ApellidoCasada { get; set; }
        public string Genero { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public string Dpi { get; set; }

        public virtual ICollection<TbUsuario> TbUsuario { get; set; }
    }
}
