using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Domain
{
    public class Permission
    {
        public int PermissionId { get; set; }
        public string Name { get; set; }
        public bool Value { get; set; }
        public DateTime Updated { get; set; }
        public virtual IEnumerable<UserPermission> UserPermissions { get; set; }
    }
}
