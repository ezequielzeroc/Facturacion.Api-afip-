using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facturacion.Models.FinancialMovements
{
    public class ConfirmModel
    {
        public int invoiceId { get; set; }
        public  bool paid { get; set; }
        public bool SendEmail { get; set; }
        public bool AttachInvoice { get; set; }
    }
}
