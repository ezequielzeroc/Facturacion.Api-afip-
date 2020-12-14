using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Domain
{
    public class IdentityDocumentType
    {
        public int IdentityDocumentTypeID { get; set; }
        public int CompanyID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? LastUpdate { get; set; }
        public Company Company { get; set; }

    }
}
