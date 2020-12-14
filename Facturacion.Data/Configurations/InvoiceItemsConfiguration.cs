using Facturacion.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Data.Configurations
{
    public class InvoiceItemsConfiguration: IEntityTypeConfiguration<InvoiceItems>
    {
        public void Configure(EntityTypeBuilder<InvoiceItems> entity)
        {
            
            entity.HasKey(x => x.ItemID);
            entity.Property(e => e.ItemID)
                .HasColumnName("ItemID")
                .ValueGeneratedOnAdd().UseIdentityColumn();
            entity.HasOne(x => x.Invoice)
                .WithMany(u => u.Items)
                .HasForeignKey(b => b.InvoiceID);

        }
    }

}
