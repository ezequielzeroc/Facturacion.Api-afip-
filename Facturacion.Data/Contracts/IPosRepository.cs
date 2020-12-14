using Facturacion.Data.Models.Account;
using Facturacion.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Enums;
namespace Facturacion.Data.Contracts
{
    public interface IPosRepository
    {
        Task<Pos> Create(Pos pos);
        Task<Pos> GetPos(int id, int CompanyId);
        Task<IEnumerable<Pos>> List(int CompanyId);
        Task<bool> Exists(int posId);
        Task<bool> Delete(int PosId, int CompanyId);
        Task<bool> Save(Pos pos);
    }
}
