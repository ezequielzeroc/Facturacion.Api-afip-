using Facturacion.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Data.Configurations
{
    public class DocumentToSendConfiguration : IEntityTypeConfiguration<DocumentToSend>
    {
        public void Configure(EntityTypeBuilder<DocumentToSend> entity)
        {
            
            entity.HasKey(x => x.DocumentToSendID);
            entity.Property(e => e.DocumentToSendID)
                .HasColumnName("DocumentToSendID")
                .ValueGeneratedOnAdd().UseIdentityColumn();

        }
    }

}
