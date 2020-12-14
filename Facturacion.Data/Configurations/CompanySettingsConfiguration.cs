using Facturacion.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Data.Configurations
{
    public class CompanySettingsConfiguration: IEntityTypeConfiguration<CompanySettings>
    {
        public void Configure(EntityTypeBuilder<CompanySettings> entity)
        {
            
            entity.HasKey(x => x.CompanySettingsID);
            entity.Property(e => e.CompanySettingsID)
                .HasColumnName("CompanySettingsID")
                .ValueGeneratedOnAdd().UseIdentityColumn();
            entity.HasOne(x => x.Company)
                 .WithOne(x => x.CompanySettings);

        }
    }

}
