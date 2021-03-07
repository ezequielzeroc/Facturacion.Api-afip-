using Facturacion.Data.Configurations;
using Facturacion.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace Facturacion.Data
{
    public class EasyStcokDBContext : DbContext
    {
        public EasyStcokDBContext() { }

        public EasyStcokDBContext(DbContextOptions<EasyStcokDBContext> options): base(options) { }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Pos> Pos { get; set; }
        public virtual DbSet<Invoices> Invoices{ get; set; }
        public virtual DbSet<InvoiceItems> InvoiceItems { get; set; }
        public virtual DbSet<IdentityDocumentType> IdentityDocumentTypes { get; set; }
        public virtual DbSet<VatCondition> VatConditions { get; set; }
        public virtual DbSet<CompanySettings> CompanySettings { get; set; }
        public virtual DbSet<Taxes> Taxes { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<Certificates> Certificates { get; set; }
        public virtual DbSet<BarCode> BarCodes { get; set; }
        public virtual DbSet<Download> Downloads { get; set; }
        public virtual DbSet<AfipError> AfipErrors { get; set; }
        public virtual DbSet<CancellationLogic> CancellationLogics{ get; set; }
        public virtual DbSet<FinancialMovements> FinancialMovements { get; set; }
        public virtual DbSet<FinancialMovementTypes> FinancialMovementTypes { get; set; }
        public virtual DbSet<DocumentToSend> DocumentToSend { get; set; }
        public virtual DbSet<UserPermission> UserPermissions { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }

        



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsersConfiguration());
            modelBuilder.ApplyConfiguration(new RolesConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new PosConfiguration());
            modelBuilder.ApplyConfiguration(new InvoicesConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceItemsConfiguration());
            modelBuilder.ApplyConfiguration(new IdentityDocumentTypeConfiguration());
            modelBuilder.ApplyConfiguration(new VatConditionsConfiguration());
            modelBuilder.ApplyConfiguration(new TaxesConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CertificatesConfiguration());
            modelBuilder.ApplyConfiguration(new BarCodeConfiguration());
            modelBuilder.ApplyConfiguration(new DownloadConfiguration());
            modelBuilder.ApplyConfiguration(new AfipErrorConfiguration());
            modelBuilder.ApplyConfiguration(new CancellationConfiguration());
            modelBuilder.ApplyConfiguration(new FinancialMovementsConfiguration());
            modelBuilder.ApplyConfiguration(new FinancialMovementTypesConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentToSendConfiguration());
            modelBuilder.ApplyConfiguration(new UserPermissionConfiguration());
        }
    }
}
