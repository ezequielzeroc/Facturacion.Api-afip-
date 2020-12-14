using Facturacion.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Data.Configurations
{
    public class InvoicesConfiguration: IEntityTypeConfiguration<Invoices>
    {
        public void Configure(EntityTypeBuilder<Invoices> entity)
        {
            
            entity.HasKey(x => x.InvoiceID);
            entity.Property(e => e.InvoiceID)
                .HasColumnName("InvoiceID")
                .ValueGeneratedOnAdd().UseIdentityColumn();
            entity.HasOne(x => x.Company)
                .WithMany(u => u.Invoices)
                .HasForeignKey(b => b.CompanyID);

            entity.HasOne(x => x.BarCode)
            .WithOne(f => f.Invoices)
            .HasForeignKey<BarCode>(x => x.InvoiceFK);

            entity.HasOne(x => x.Download)
            .WithOne(f => f.Invoices)
            .HasForeignKey<Download>(x => x.InvoiceFK);

            entity.HasOne(x => x.DocumentType)
            .WithMany(u => u.Invoices)
            .HasForeignKey(b => b.DocumentTypeID);


        }
    }

}
