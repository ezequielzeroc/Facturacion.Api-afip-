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
    public class PosRepository : IPosRepository
    {
        private EasyStcokDBContext _dbContex;
        public PosRepository(EasyStcokDBContext dbContext)
        {
            _dbContex = dbContext;
        }

        public async Task<Pos> Create(Pos pos)
        {
            try
            {
                _dbContex.Entry(pos).State = EntityState.Added;
                await _dbContex.SaveChangesAsync();
                return pos;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> Delete(int PosId, int CompanyId)
        {
            bool ret = true;
            try
            {
                Pos pos = await _dbContex.Pos.FirstOrDefaultAsync(x => x.PosId == PosId && x.CompanyId == x.CompanyId);
                _dbContex.Entry(pos).State = EntityState.Deleted;
                ret = await _dbContex.SaveChangesAsync() > 0;
                return ret;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public Task<bool> Exists(int posId)
        {
            throw new NotImplementedException();
        }

        public async Task<Pos> GetPos(int id, int CompanyId)
        {
            Pos posResult = null;
            try
            {
                posResult = await _dbContex.Pos.FirstOrDefaultAsync(x => x.CompanyId == CompanyId && x.PosId == id);
                return posResult;
            }
            catch ( Exception e)
            {
                return posResult;
            }
        }

        public async Task<IEnumerable<Pos>> List(int CompanyId)
        {
            IEnumerable<Pos> list;
            IQueryable<Pos> QList;
            IQueryable<Pos> QResult;
            try
            {
                QList = _dbContex.Pos.AsQueryable();
                QResult = QList.Where(x => x.CompanyId == CompanyId);
                return await QResult.ToListAsync();
            }
            catch (Exception e )
            {

                throw;
            }
        }

        public async Task<bool> Save(Pos pos)
        {
            bool ret = true;
            Pos posToEdit = null;
            try
            {
                posToEdit = await _dbContex.Pos.FirstOrDefaultAsync(x => x.PosId == pos.PosId && x.CompanyId == pos.CompanyId);
                posToEdit.Code = pos.Code;
                posToEdit.BusinessName = pos.BusinessName;
                posToEdit.Address = pos.Address;
                posToEdit.Description = pos.Description;
                posToEdit.Name = pos.Name;

                _dbContex.Entry(posToEdit).State = EntityState.Modified;
                
                ret = await _dbContex.SaveChangesAsync() > 0;
                return ret;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
