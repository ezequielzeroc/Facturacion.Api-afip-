using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Data.Models.Account
{
    public class ChangePasswordModel
    {
        public string UserName { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
        public string confirmNewPassword { get; set; }
    }
}
