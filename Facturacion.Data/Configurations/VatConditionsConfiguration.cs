using Facturacion.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Data.Configurations
{
    public class VatConditionsConfiguration : IEntityTypeConfiguration<VatCondition>
    {
        public void Configure(EntityTypeBuilder<VatCondition> entity)
        {
            
            entity.HasKey(x => x.VatConditionID);
            entity.Property(e => e.VatConditionID)
                .HasColumnName("VatConditionID")
                .ValueGeneratedOnAdd().UseIdentityColumn();
            entity.HasOne(x => x.Company)
                .WithMany(u => u.VatConditions)
                .HasForeignKey(b => b.CompanyID);

        }
    }

}
