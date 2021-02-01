using Facturacion.Data.Models.Account;
using Facturacion.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Enums;
namespace Facturacion.Data.Contracts
{
    public interface IFinancialMovementsRepository
    {

        Task<decimal> Get(int CompanyID, int MovementType);
        Task<bool> MarkAsPaid(int CompanyID, int InvoiceID);
    }
}
