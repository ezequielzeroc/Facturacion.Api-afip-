using Facturacion.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Data.Configurations
{
    public class IdentityDocumentTypeConfiguration : IEntityTypeConfiguration<IdentityDocumentType>
    {
        public void Configure(EntityTypeBuilder<IdentityDocumentType> entity)
        {
            
            entity.HasKey(x => x.IdentityDocumentTypeID);
            entity.Property(e => e.IdentityDocumentTypeID)
                .HasColumnName("IdentityDocumentTypeID")
                .ValueGeneratedOnAdd().UseIdentityColumn();
            entity.HasOne(x => x.Company)
                .WithMany(u => u.IdentityDocumentTypes)
                .HasForeignKey(b => b.CompanyID);

        }
    }

}
