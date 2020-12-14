using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Domain
{
    public class Download
    {
        public int DownloadID { get; set; }
        public int InvoiceFK { get; set; }
        public string File { get; set; }
        public DateTime Created { get; set; }
        public DateTime Downloaded { get; set; }
        public int Count { get; set; }
        public Invoices Invoices { get; set; }
    }
}
