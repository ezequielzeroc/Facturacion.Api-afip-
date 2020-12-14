using Facturacion.Data.Models.Account;
using Facturacion.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Enums;
namespace Facturacion.Data.Contracts
{
    public interface IAccountRepository
    {
        Task<Account.Login> ValidateLogin(LoginModel login);
        Task<Account.Create> Create(UserCreateModel ToCreate);
        Task<bool> Exists(string userName);
        Task<Account.Delete> Delete(int UserId);
        Task<Account.ChangePassword> ChangePassword(ChangePasswordModel passwords);
        Task<Users> GetUser(LoginModel login);
    }
}
