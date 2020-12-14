using Facturacion.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Data.Configurations
{
    public class DocumentTypeConfiguration : IEntityTypeConfiguration<DocumentType>
    {
        public void Configure(EntityTypeBuilder<DocumentType> entity)
        {
            
            entity.HasKey(x => x.DocumentTypeID);
            entity.Property(e => e.DocumentTypeID)
                .HasColumnName("DocumentTypeID")
                .ValueGeneratedOnAdd().UseIdentityColumn();
    
        }
    }

}
