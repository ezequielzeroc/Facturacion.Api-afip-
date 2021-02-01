using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Domain
{
   public class DocumentToSend
    {
        public int DocumentToSendID { get; set; }
        public int InvoiceID { get; set; }
        public DateTime Date { get; set; }
        public string ToName { get; set; }
        public string Subject { get; set; }
        public string To { get; set; }
        public string Message { get; set; }
        public bool AttachInvoice { get; set; }
        public int PriorityLevel { get; set; }
        public int TimesSent { get; set; }
        public bool Sent { get; set; }
    }
}
