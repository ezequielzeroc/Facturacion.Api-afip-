using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Domain
{
    public class AfipError
    {
        public int AfipErrorID { get; set; }
        public int Code { get; set; }
        public string Description { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
