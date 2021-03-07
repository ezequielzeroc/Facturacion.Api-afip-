using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Data.Models.Account
{
    public class LoginResponse : ResponseModel
    {
        public string token { get; set; }
        public string name { get; set; }
        public bool isAdmin { get; set; }
        public List<Tuple<string, string>> Roles { get; set; }
        public List<string> Permissions { get; set; }


    }
}
