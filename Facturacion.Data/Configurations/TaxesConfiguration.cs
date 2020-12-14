using Facturacion.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Data.Configurations
{
    public class TaxesConfiguration : IEntityTypeConfiguration<Taxes>
    {
        public void Configure(EntityTypeBuilder<Taxes> entity)
        {
            entity.HasKey(x => x.TaxId);
            entity.Property(e => e.TaxId)
                .HasColumnName("TaxId")
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

        }
    }
}
