using Facturacion.Data.Contracts;
using Facturacion.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.Data.Repositories
{
    //public class CompanyRepository : ICompanyRepository
    //{
    //    private readonly EasyStcokDBContext _dbContext;
    //    public CompanyRepository(EasyStcokDBContext dbContext)
    //    {
    //        _dbContext = dbContext;
    //    }
    //    public Task<CompanySettings> getSettings(int CompanyID)
    //    {
    //        CompanySettings settings = null;
    //        try
    //        {
    //            settings = _dbContext.CompanySettings.FindAsync(x=>x.comp)
    //        }
    //        catch (Exception ex)
    //        {

    //            throw;
    //        }
    //    }

    //    public Task<bool> Update(CompanySettings settings)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
