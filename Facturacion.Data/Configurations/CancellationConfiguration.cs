using Facturacion.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Data.Configurations
{
    public class CancellationConfiguration : IEntityTypeConfiguration<CancellationLogic>
    {
        public void Configure(EntityTypeBuilder<CancellationLogic> entity)
        {
            
            entity.HasKey(x => x.CancellationLogicID);
            entity.Property(e => e.CancellationLogicID)
                .HasColumnName("CancellationLogicID")
                .ValueGeneratedOnAdd().UseIdentityColumn();

        }
    }

}
