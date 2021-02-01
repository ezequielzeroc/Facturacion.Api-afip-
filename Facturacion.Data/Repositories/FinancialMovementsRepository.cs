using Facturacion.Data.Contracts;
using Facturacion.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.Data.Repositories
{
    public class FinancialMovementsRepository:IFinancialMovementsRepository
    {
        private readonly EasyStcokDBContext _dbContext;
        public FinancialMovementsRepository(EasyStcokDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<decimal> Get(int CompanyID, int MovementType)
        {
            /// 1 int
            /// 2 out
            IQueryable<FinancialMovements> quer;
            decimal result = 0;
            try
            {
                quer = _dbContext.FinancialMovements.AsQueryable();
                quer = quer.Where(x => x.CompanyID == CompanyID && x.TypeID == MovementType);
                result = quer.Sum(x => x.Ammount);
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<bool> MarkAsPaid(int CompanyID, int InvoiceID)
        {
            bool ret = true;
            Invoices invoice;
            FinancialMovements newMovement;
            try
            {
                invoice = await _dbContext.Invoices.FirstOrDefaultAsync(x => x.CompanyID == CompanyID && x.InvoiceID == InvoiceID);
                if (invoice != null)
                {
                    invoice.Paid = true;
                    ret = await _dbContext.SaveChangesAsync()>0;
                    newMovement = await _dbContext.FinancialMovements.Where(x => x.InvoiceID == invoice.InvoiceID).FirstOrDefaultAsync();
                    
                    if(newMovement!=null)
                    {
                        newMovement.TypeID = 1;
                        _dbContext.Entry(newMovement).State = EntityState.Modified;
                    }
                    else {
                        newMovement = new FinancialMovements
                        {
                            CompanyID = CompanyID,
                            isCompleted = true,
                            Ammount = invoice.Total,
                            Date = DateTime.Now,
                            Description = "Factura cobrada",
                            TypeID = 1,
                            InvoiceID = invoice.InvoiceID

                        };
                        
                        _dbContext.Entry(newMovement).State = EntityState.Added;
                    }
                    
                    await _dbContext.SaveChangesAsync();
                }


                
                return ret;
            }
            catch (Exception e) 
            {

                return false;
            }
        }
    }
}
