using Facturacion.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Data.Configurations
{
    public class UserPermissionConfiguration : IEntityTypeConfiguration<UserPermission>
    {
        public void Configure(EntityTypeBuilder<UserPermission> entity)
        {
            
            entity.HasOne(p => p.Permissions)
                .WithMany(u => u.UserPermissions)
                .HasForeignKey(x => x.PermissionId);

            entity.HasOne(p => p.Users)
                .WithMany(u => u.UserPermissions)
                .HasForeignKey(x => x.UserID);
        }
    }

}