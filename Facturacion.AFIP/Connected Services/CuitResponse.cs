using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facturacion.AFIP
{
  public  class CuitResponse
    {
        private string[] CondicionesIVA = { "Monotributo", "Responsable Inscripto", "IVA Exento", "Consumidor Final" };

        public string Name { get; set; }
        public string LastName { get; set; }
        public string BusinessName { get; set; }
        public string PostalCode { get; set; }
        public int ProvinceID { get; set; }
        public int ProvinceDesc { get; set; }
        public int TaxID { get; set; }
        public string TaxDesc{ get; set; }
        public int PersonType { get; set; }
        public string PersonTypeDesc { get; set; }
        public int DocumentType { get; set; }
        public string DocumentNro { get; set; }
        public string FiscalAddress { get; set; }

    }
}
