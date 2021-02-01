using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Domain
{
    public class FinancialMovements
    {
        
        public int MovementID { get; set; }
        public int InvoiceID { get; set; }
        public int CompanyID { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Ammount { get; set; }
        public int TypeID { get; set; }
        public bool isCompleted { get; set; }
        public FinancialMovementTypes Type { get; set; }
        public Invoices Invoice { get; set; }
    }
}
