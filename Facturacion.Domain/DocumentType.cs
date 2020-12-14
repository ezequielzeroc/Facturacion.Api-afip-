using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Domain
{
    public class DocumentType
    {
        public int DocumentTypeID { get; set; }
        public int TypeOfResponsibleID { get; set; }
        public int Code { get; set; }
        public string Letter { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Invoices> Invoices { get; set; }
    }
}
