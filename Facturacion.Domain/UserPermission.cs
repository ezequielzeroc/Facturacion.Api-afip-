using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Domain
{
    public class UserPermission
    {
        public int Id { get; set; }
        public Permission Permissions { get; set; }
        public int PermissionId { get; set; }
        public Users Users { get; set; }
        public int UserID { get; set; } //userId


    }
}
