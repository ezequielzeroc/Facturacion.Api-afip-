using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Domain
{
    public class CancellationLogic
    {
        public int CancellationLogicID { get; set; }
        public int OriginalCode { get; set; }
        public int CancellationCode { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
