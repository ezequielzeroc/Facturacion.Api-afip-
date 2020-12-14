using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Domain
{
    public class CompanySettings
    {
        public int CompanySettingsID { get; set; }
        public long Cuit { get; set; }
        public string BusinessName { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string FiscalAddress { get; set; }
        public string GrossIncomeNumber { get; set; }
        public DateTime StartActivities { get; set; }
        public int TaxCategoryId { get; set; }
        public string DocumentLogo { get; set; }
        public virtual int CompanyID { get; set; }
        public virtual Company Company { get; set; }

    }
}
