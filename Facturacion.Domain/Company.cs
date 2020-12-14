using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Domain
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string BusinessName { get; set; }
        public string  Address { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual List<Users> Users { get; set; }
        public virtual List<Pos> Pos { get; set; }
        public List<Invoices> Invoices { get; set; }
        public List<IdentityDocumentType> IdentityDocumentTypes { get; set; }
        public List<VatCondition> VatConditions { get; set; }
        public CompanySettings CompanySettings { get; set; }
    }
}
