﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Data.Models.Account
{
    [Serializable]
    public class LoginModel
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}
