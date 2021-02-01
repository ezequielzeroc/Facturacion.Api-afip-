using Facturacion.Data.Models.Invoices;
using Facturacion.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.Data.Contracts
{
    public interface IInvoiceRepository
    {
   
        Task<InvoiceResponseModel> Create(int CompanyID, Invoices invoice);
        Task<IEnumerable<Invoices>> GetInvoices(int CompanyID);
        Task<FileStream> Download(int InvoiceID);
        Task<bool> Delete(int InvoiceID);
        Task<InvoiceResponseModel> CancelInvoice(int CompanyID, int InvoiceID);

    }
}
