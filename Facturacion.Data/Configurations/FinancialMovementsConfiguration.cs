using Facturacion.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Data.Configurations
{
    public class FinancialMovementsConfiguration : IEntityTypeConfiguration<FinancialMovements>
    {
        public void Configure(EntityTypeBuilder<FinancialMovements> entity)
        {
            
            entity.HasKey(x => x.MovementID);
            entity.Property(e => e.MovementID)
                .HasColumnName("MovementID")
                .ValueGeneratedOnAdd().UseIdentityColumn();
            entity.HasOne(x => x.Type)
                .WithMany(f=>f.FinancialMovements)
                .HasForeignKey(b =>b.TypeID);

            entity.HasOne(x => x.Invoice)
               .WithMany(u => u.financialMovements)
               .HasForeignKey(b => b.InvoiceID);
        }
    }

}
