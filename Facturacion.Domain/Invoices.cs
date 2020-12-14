using Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Domain
{
    public class Invoices
    {
        public int InvoiceID { get; set; }
        public int InvoiceNumber { get; set; }
        public int ClientID { get; set; }
        public int CompanyID { get; set; }
        public int StoreID { get; set; }
        public int PosCode { get; set; }
        public string DocumentTypeCode { get; set; }
        public string DocumentTypeShortCode { get; set; }
        public int DocumentTypeID { get; set; }
        public string Letter { get; set; }
        public string ConceptCode { get; set; }
        public string VatConditionCode { get; set; }
        public string IdentityDocumentTypeCode { get; set; }
        public string IdentityDocumentNumber { get; set; }
        public string ClientName { get; set; }
        public string ClientAddress { get; set; }
        public string ClientEmail { get; set; }
        public InvoiceStatus Status { get; set; }
        public decimal Total { get; set; }
        public decimal Subtotal { get; set; }
        public decimal TotalTaxes { get; set; }
        public decimal TotalDiscount { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastUpdate { get; set; }
        public bool Printed { get; set; }
        public bool SentEmail { get; set; }
        public string CAE { get; set; }
        public DateTime? CAEExpiration { get; set; }
        public DateTime? ExpirationDay { get; set; }
        public List<InvoiceItems> Items { get; set; }
        public Company Company { get; set; }
        public DocumentType DocumentType { get; set; }
        public BarCode BarCode { get; set; }
        public Download Download{ get; set; }
    }
}
