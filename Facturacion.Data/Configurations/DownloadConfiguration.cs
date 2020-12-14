using Facturacion.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Data.Configurations
{
    public class DownloadConfiguration : IEntityTypeConfiguration<Download>
    {
        public void Configure(EntityTypeBuilder<Download> entity)
        {
            
            entity.HasKey(x => x.DownloadID);
            entity.Property(e => e.DownloadID)
                .HasColumnName("DownloadID")
                .ValueGeneratedOnAdd().UseIdentityColumn();
        }
    }

}
