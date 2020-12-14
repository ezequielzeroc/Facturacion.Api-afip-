using Facturacion.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Data.Configurations
{
    public class RolesConfiguration : IEntityTypeConfiguration<Roles>
    {
        public void Configure(EntityTypeBuilder<Roles> entity)
        {
            entity.HasKey(x => x.RoleID);
            entity.Property(e => e.RoleID)
                .HasColumnName("RoleID")
                .ValueGeneratedOnAdd().UseIdentityColumn();

        }
    }
}
