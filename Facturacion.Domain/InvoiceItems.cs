using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Domain
{
    public class InvoiceItems
    {
        public int ItemID { get; set; }
        public int InvoiceID { get; set; }
        public string Description { get; set; }
        public int Qtty { get; set; }
        public decimal Price { get; set; }
        public decimal TaxId { get; set; }
        public decimal TaxPercent { get; set; }
        public decimal TaxCalculated { get; set; }
        public decimal CalculatedDiscount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal Discount  { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public int UnitOfMeasurement { get; set; }
        public Invoices Invoice { get; set; }


    }
}
