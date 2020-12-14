using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Domain
{
    public class BarCode
    {
        public int BarCodeID { get; set; }
        public int InvoiceFK { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public Invoices Invoices { get; set; }
    }
}
