using Facturacion.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Data.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> entity)
        {
            entity.HasKey(x => x.CompanyId);
            entity.Property(e => e.CompanyId)
                .HasColumnName("CompanyId")
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

        }
    }
}
