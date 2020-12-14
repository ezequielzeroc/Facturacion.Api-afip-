using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Data.Models.Invoices
{
    public partial class InvoiceResponseModel : FEAFIPResponse
    {
        public int InvoiceID { get; set; }
        public DateTime Created { get; set; }
        public DateTime PaymentDueDate { get; set; }
    }

    public partial class FEAFIPResponse
    {
        public string CAE { get; set; }
        public string Message { get; set; }
        public string DueDateCae { get; set; }
        public string Result { get; set; }
        public IEnumerable<InvoiceAuthorizationObs> Observations { get; set; }
    }

    public partial class InvoiceAuthorizationObs
    {
        public int Code { get; set; }
        public string Description { get; set; }
    }
}
