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

    public class IdentityDocumentTypeRepository: IIdentityDocumentTypeRepository
    {
        private EasyStcokDBContext _dbContex;
        public IdentityDocumentTypeRepository(EasyStcokDBContext dbContext)
        {
            _dbContex = dbContext;
        }

        public async Task<IEnumerable<IdentityDocumentType>> List(int CompanyId)
        {
            IEnumerable<IdentityDocumentType> list;
            IQueryable<IdentityDocumentType> dbQuery;
            try
            {
                dbQuery = _dbContex.IdentityDocumentTypes.AsQueryable();
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
