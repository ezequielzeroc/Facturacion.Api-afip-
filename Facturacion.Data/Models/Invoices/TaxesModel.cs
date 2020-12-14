using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Data.Models.Invoices
{
    public class TaxesModel
    {
        public int TaxID { get; set; }
        public double TaxBase { get; set; }
        public double CalculatedTax { get; set; }
    }
}
