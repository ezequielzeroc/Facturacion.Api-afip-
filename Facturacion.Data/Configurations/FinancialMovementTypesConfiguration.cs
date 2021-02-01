using Facturacion.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Data.Configurations
{
    public class FinancialMovementTypesConfiguration : IEntityTypeConfiguration<FinancialMovementTypes>
    {
        public void Configure(EntityTypeBuilder<FinancialMovementTypes> entity)
        {
            
            entity.HasKey(x => x.MovementTypeID);
            entity.Property(e => e.MovementTypeID)
                .HasColumnName("MovementTypeID")
                .ValueGeneratedOnAdd().UseIdentityColumn();

        }
    }

}
