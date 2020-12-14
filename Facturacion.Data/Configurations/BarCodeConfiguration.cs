using Facturacion.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Data.Configurations
{
    public class BarCodeConfiguration : IEntityTypeConfiguration<BarCode>
    {
        public void Configure(EntityTypeBuilder<BarCode> entity)
        {
            
            entity.HasKey(x => x.BarCodeID);
            entity.Property(e => e.BarCodeID)
                .HasColumnName("BarCodeID")
                .ValueGeneratedOnAdd().UseIdentityColumn();
        }
    }

}
