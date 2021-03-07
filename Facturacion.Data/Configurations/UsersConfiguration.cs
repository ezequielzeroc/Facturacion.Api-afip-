using Facturacion.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Data.Configurations
{
    public class UsersConfiguration: IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> entity)
        {
            
            entity.HasKey(x => x.UserId);
            entity.Property(e => e.UserId)
                .HasColumnName("UserId")
                .ValueGeneratedOnAdd().UseIdentityColumn();
            entity.HasOne(x => x.Role)
                .WithMany(u => u.Users)
                .HasForeignKey(b => b.RoleId);
            entity.HasOne(x => x.Company)
                .WithMany(u => u.Users)
                .HasForeignKey(b => b.CompanyId);
        }
    }

}
