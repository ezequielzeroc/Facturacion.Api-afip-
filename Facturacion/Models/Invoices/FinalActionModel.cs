using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facturacion.Models.Invoices
{
    public class FinalActionModel : Domain.DocumentToSend
    {
        public int invoiceId { get; set; }
        public bool paid { get; set; }
        public bool SendEmail { get; set; }

    }
}
