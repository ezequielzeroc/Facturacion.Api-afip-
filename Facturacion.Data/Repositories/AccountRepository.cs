using Facturacion.Data.Contracts;
using Facturacion.Data.Models.Account;
using Facturacion.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Enums;

namespace Facturacion.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private EasyStcokDBContext _dbContex;
        private IPasswordHasher<Users> _passwordHasher;
        public AccountRepository(EasyStcokDBContext dBContext, IPasswordHasher<Users> passwordHasher)
        {
            _dbContex = dBContext;
            _passwordHasher = passwordHasher;
        }

        public async Task<Account.ChangePassword> ChangePassword(ChangePasswordModel passwords)
        {
            try
            {
                string oldPassword = string.Empty;
                string newPassword = string.Empty;
                Users user = await _dbContex.Users
                   .FirstOrDefaultAsync(u => u.UserName == passwords.UserName);
                if (user != null)
                {
                    PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(user, user.Password, passwords.oldPassword);
                    if (result == PasswordVerificationResult.Success)
                    {
                        if (passwords.newPassword == passwords.confirmNewPassword)
                        {
                            user.Password = _passwordHasher.HashPassword(user,passwords.newPassword);
                            _dbContex.Entry(user).State = EntityState.Modified;
                            if(await _dbContex.SaveChangesAsync() == 0)
                            {
                                return Account.ChangePassword.Not_Changed;
                            }
                        }
                        else
                        {
                            return Account.ChangePassword.Password_Dont_Match;
                        }
                    }
                    else
                    {
                        return Account.ChangePassword.Password_Verification_Failure;
                    }
                }
                else
                {
                    return Account.ChangePassword.Password_Not_Linked;
                }
                return Account.ChangePassword.Changed;

            }
            catch (Exception)
            {
                return Account.ChangePassword.Unknown_Error;
            }
        }

        public async Task<Account.Create> Create(UserCreateModel ToCreate)
        {
            Roles role = null;
            Users user = null;
            Users userRet = null;
            Company company = null;
            bool ret = true;
            UserCreateResponseModel response;

            try
            {
                if(await Exists(ToCreate.UserName))
                {
                    return Enums.Account.Create.Account_Exists;
                }
                role = await _dbContex.Roles.FirstOrDefaultAsync(x => x.Name == "Administrador");
                company = await _dbContex.Companies.FirstOrDefaultAsync(x => x.CompanyId == 1);
                if (role == null)
                {
  
                    return  Enums.Account.Create.Not_Created_Rol_Not_Exists;
                }
                user = new Users
                {
                    Email = ToCreate.Email,
                    UserName = ToCreate.UserName,
                    Name = ToCreate.Name,
                    LastName = ToCreate.LastName,
                    Password = _passwordHasher.HashPassword(user, ToCreate.Password),
                    Role = role,
                    Company = company 
                };
 
                _dbContex.Entry(user).State = EntityState.Added;
                ret = await _dbContex.SaveChangesAsync() > 0;
                if (ret)
                {
                    return Account.Create.Created;
                }
                else
                {
                    return Account.Create.Not_Created;
                }

            }
            catch (Exception e)
            {
                return Account.Create.Unknown_Error;
            }

        }

        public async Task<Account.Delete> Delete(int UserId)
        {
            Users user;
            try
            {
                user = await _dbContex.Users.FirstOrDefaultAsync(x => x.Id == UserId);
                if (user == null)
                {
                    return Account.Delete.User_Not_Found;
                }
                _dbContex.Entry(user).State = EntityState.Deleted;
                if(await _dbContex.SaveChangesAsync() == 0)
                {
                    return Account.Delete.Not_Deleted;
                }
                else
                {
                    return Account.Delete.Deleted;
                }
            }
            catch (Exception e)
            {
                return Account.Delete.Error_Deleting;
            }
        }

        public async Task<bool> Exists(string userName)
        {
           
            Users user = null;
            try
            {
                user = await _dbContex.Users.FirstOrDefaultAsync(x=>x.UserName == userName);
                return user != null;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<Users> GetUser(LoginModel login)
        {
            Users user = null;
            string _token = string.Empty;
            try
            {
                user = await _dbContex.Users.FirstOrDefaultAsync(x => x.UserName == login.username);
                return user;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<Account.Login> ValidateLogin(LoginModel login)
        {
            LoginResponse response = new LoginResponse();
            var user = await _dbContex.Users
           .Include(x => x.Company)
           .FirstOrDefaultAsync(u => u.UserName == login.username);
            try
            {
                if (user != null)
                {
                    var rest = _passwordHasher.VerifyHashedPassword(user, user.Password, login.password);
                    if(rest == PasswordVerificationResult.Success)
                    {
                        return  Account.Login.Success;
                    }
                    else
                    {
                        return Account.Login.User_Password_Error;
                    }
                }
                else
                {
                    return Account.Login.User_Password_Error;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
