using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Domain
{
    public class Pos
    {
        public int PosId { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string BusinessName { get; set; }
        public virtual int CompanyId { get; set; }
        public virtual  Company Company { get; set; }
    }
}
