using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Facturacion.Domain
{
    public class Users
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsSuperAdmin { get; set; }
        public virtual int CompanyId { get; set; }
        public virtual int RoleId { get; set; }
        public virtual  Roles Role { get; set; }
        public virtual  Company Company { get; set; }
        public virtual IEnumerable<UserPermission> UserPermissions { get; set; }

    }
}
