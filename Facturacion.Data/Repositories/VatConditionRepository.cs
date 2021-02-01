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

    public class VatConditionRepository: IVatConditionRepository
    {
        private readonly EasyStcokDBContext _dbContex;
        public VatConditionRepository(EasyStcokDBContext dbContext)
        {
            _dbContex = dbContext;
        }

        public async Task<IEnumerable<VatCondition>> List(int CompanyId)
        {
            IEnumerable<VatCondition> list;
            IQueryable<VatCondition> dbQuery;
            try
            {
                dbQuery = _dbContex.VatConditions.AsQueryable();
                dbQuery.Where(x => x.CompanyID == CompanyId);
                list = await dbQuery.ToListAsync();
                return list;
            }
            catch (Exception e) 
            {

                return null;
            }
        }
    }
}
