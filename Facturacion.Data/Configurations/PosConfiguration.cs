using Facturacion.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Data.Configurations
{
    public class PosConfiguration: IEntityTypeConfiguration<Pos>
    {
        public void Configure(EntityTypeBuilder<Pos> entity)
        {
            
            entity.HasKey(x => x.PosId);
            entity.Property(e => e.PosId)
                .HasColumnName("PosId")
                .ValueGeneratedOnAdd().UseIdentityColumn();
            entity.HasOne(x => x.Company)
                .WithMany(p => p.Pos)
                .HasForeignKey(b => b.CompanyId);
        }
    }

}
