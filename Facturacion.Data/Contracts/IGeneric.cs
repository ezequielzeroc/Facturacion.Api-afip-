using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.Data.Contracts
{
    public interface IGeneric<TEntity>
    {
        public Task<TEntity> GetPaginated();
        public Task<int> Add(TEntity entity);
        public Task<bool> Delete(int id);
    }
}
