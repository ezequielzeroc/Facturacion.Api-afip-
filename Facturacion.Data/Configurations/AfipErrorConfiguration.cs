using Facturacion.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Data.Configurations
{
    public class AfipErrorConfiguration : IEntityTypeConfiguration<AfipError>
    {
        public void Configure(EntityTypeBuilder<AfipError> entity)
        {
            
            entity.HasKey(x => x.AfipErrorID);
            entity.Property(e => e.AfipErrorID)
                .HasColumnName("AfipErrorID")
                .ValueGeneratedOnAdd().UseIdentityColumn();
        }
    }

}
