using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Data.Models.Account
{
    public class UserResponseModel
    {
        public int Id { get; set; }
        public int CId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
        public string ImageProfile { get; set; }
    }
}
