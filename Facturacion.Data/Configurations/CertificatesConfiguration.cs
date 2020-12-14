using Facturacion.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Data.Configurations
{
    public class CertificatesConfiguration : IEntityTypeConfiguration<Certificates>
    {
        public void Configure(EntityTypeBuilder<Certificates> entity)
        {
            entity.HasKey(x => x.CertificateID);
            entity.Property(e => e.CertificateID)
                .HasColumnName("CertificateID")
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

        }
    }
}
