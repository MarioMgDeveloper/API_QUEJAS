using System;
using System.Collections.Generic;

namespace API_QUEJAS.Models
{
    public partial class TbMunicipio
    {
        public int IdMunicipio { get; set; }
        public string CodigoMunicipio { get; set; }
        public int IdDepartamento { get; set; }
        public string NombreMunicipio { get; set; }

        public virtual TbDepartamento IdDepartamentoNavigation { get; set; }
    }
}
