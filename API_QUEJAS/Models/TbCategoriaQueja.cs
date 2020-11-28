using System;
using System.Collections.Generic;

namespace API_QUEJAS.Models
{
    public partial class TbCategoriaQueja
    {
        public TbCategoriaQueja()
        {
            TbQueja = new HashSet<TbQueja>();
        }

        public int IdCategoriaQueja { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<TbQueja> TbQueja { get; set; }
    }
}
