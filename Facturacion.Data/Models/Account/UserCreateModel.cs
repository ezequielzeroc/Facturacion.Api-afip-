using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Data.Models.Account
{
    public class UserCreateModel
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
