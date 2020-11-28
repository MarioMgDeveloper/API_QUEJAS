using System;
using System.Collections.Generic;

namespace API_QUEJAS.Models
{
    public partial class TbQueja
    {
        public int IdQueja { get; set; }
        public string Descripcion { get; set; }
        public int? IdCategoriaQueja { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int IdEstado { get; set; }
        public int? IdUsuarioResuleve { get; set; }
        public string DescripcionResuelve { get; set; }
        public int? IdEstablecimiento { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual TbCategoriaQueja IdCategoriaQuejaNavigation { get; set; }
        public virtual TbEstablecimiento IdEstablecimientoNavigation { get; set; }
        public virtual TbEstado IdEstadoNavigation { get; set; }
        public virtual TbUsuario IdUsuarioResuleveNavigation { get; set; }
    }
}
