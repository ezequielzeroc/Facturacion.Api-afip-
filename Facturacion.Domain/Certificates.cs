using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Domain
{
    public class Certificates
    {
        public int CertificateID { get; set; }
        public int CompanyID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Password { get; set; }
        public string Path { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ValidaUntil { get; set; }
        public Company Company { get; set; }

    }
}
