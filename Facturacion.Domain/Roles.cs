using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Domain
{
    public class Roles
    {
        public int RoleID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public virtual List<Users> Users { get; set; }
    }
}
