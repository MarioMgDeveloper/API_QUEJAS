using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_QUEJAS.Helpers
{
    public class Cls_Response<T>
    {
        public bool Error { get; set; } = false;
        public string Message { get; set; }
        public T Body { get; set; }
    }
}
