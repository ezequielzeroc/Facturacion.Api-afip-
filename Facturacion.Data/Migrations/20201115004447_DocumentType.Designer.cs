﻿// <auto-generated />
using System;
using Facturacion.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Facturacion.Data.Migrations
{
    [DbContext(typeof(EasyStcokDBContext))]
    [Migration("20201115004447_DocumentType")]
    partial class DocumentType
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Facturacion.Domain.Company", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CompanyId")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("BusinessName")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("CompanyId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Facturacion.Domain.CompanySettings", b =>
                {
                    b.Property<int>("CompanySettingsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("BusinessName")
                        .HasColumnType("text");

                    b.Property<int>("CompanyID")
                        .HasColumnType("integer");

                    b.Property<long>("Cuit")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("DocumentLogo")
                        .HasColumnType("text");

                    b.Property<string>("FiscalAddress")
                        .HasColumnType("text");

                    b.Property<string>("GrossIncomeNumber")
                        .HasColumnType("text");

                    b.Property<string>("ShortName")
                        .HasColumnType("text");

                    b.Property<DateTime>("StartActivities")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("TaxCategoryId")
                        .HasColumnType("integer");

                    b.HasKey("CompanySettingsID");

                    b.HasIndex("CompanyID")
                        .IsUnique();

                    b.ToTable("CompanySettings");
                });

            modelBuilder.Entity("Facturacion.Domain.DocumentType", b =>
                {
                    b.Property<int>("DocumentTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("DocumentTypeID")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Code")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Letter")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("TypeOfResponsibleID")
                        .HasColumnType("integer");

                    b.HasKey("DocumentTypeID");

                    b.ToTable("DocumentTypes");
                });

            modelBuilder.Entity("Facturacion.Domain.IdentityDocumentType", b =>
                {
                    b.Property<int>("IdentityDocumentTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IdentityDocumentTypeID")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<int>("CompanyID")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastUpdate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("IdentityDocumentTypeID");

                    b.HasIndex("CompanyID");

                    b.ToTable("IdentityDocumentTypes");
                });

            modelBuilder.Entity("Facturacion.Domain.InvoiceItems", b =>
                {
                    b.Property<int>("ItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ItemID")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<decimal>("CalculatedDiscount")
                        .HasColumnType("numeric");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<decimal>("Discount")
                        .HasColumnType("numeric");

                    b.Property<int>("InvoiceID")
                        .HasColumnType("integer");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("Qtty")
                        .HasColumnType("integer");

                    b.Property<decimal>("SubTotal")
                        .HasColumnType("numeric");

                    b.Property<decimal>("TaxCalculated")
                        .HasColumnType("numeric");

                    b.Property<decimal>("TaxId")
                        .HasColumnType("numeric");

                    b.Property<decimal>("TaxPercent")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Total")
                        .HasColumnType("numeric");

                    b.Property<decimal>("TotalDiscount")
                        .HasColumnType("numeric");

                    b.Property<int>("UnitOfMeasurement")
                        .HasColumnType("integer");

                    b.HasKey("ItemID");

                    b.HasIndex("InvoiceID");

                    b.ToTable("InvoiceItems");
                });

            modelBuilder.Entity("Facturacion.Domain.Invoices", b =>
                {
                    b.Property<int>("InvoiceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("InvoiceID")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClientAddress")
                        .HasColumnType("text");

                    b.Property<string>("ClientEmail")
                        .HasColumnType("text");

                    b.Property<int>("ClientID")
                        .HasColumnType("integer");

                    b.Property<string>("ClientName")
                        .HasColumnType("text");

                    b.Property<int>("CompanyID")
                        .HasColumnType("integer");

                    b.Property<string>("ConceptCode")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("IdentityDocumentNumber")
                        .HasColumnType("text");

                    b.Property<string>("IdentityDocumentTypeCode")
                        .HasColumnType("text");

                    b.Property<string>("InvoiceNumber")
                        .HasColumnType("text");

                    b.Property<string>("InvoiceTypeCode")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastUpdate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("PosCode")
                        .HasColumnType("integer");

                    b.Property<bool>("Printed")
                        .HasColumnType("boolean");

                    b.Property<bool>("SentEmail")
                        .HasColumnType("boolean");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("StoreID")
                        .HasColumnType("integer");

                    b.Property<decimal>("Subtotal")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Total")
                        .HasColumnType("numeric");

                    b.Property<decimal>("TotalDiscount")
                        .HasColumnType("numeric");

                    b.Property<decimal>("TotalTaxes")
                        .HasColumnType("numeric");

                    b.Property<string>("VatConditionCode")
                        .HasColumnType("text");

                    b.HasKey("InvoiceID");

                    b.HasIndex("CompanyID");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("Facturacion.Domain.Pos", b =>
                {
                    b.Property<int>("PosId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("PosId")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("BusinessName")
                        .HasColumnType("text");

                    b.Property<int>("Code")
                        .HasColumnType("integer");

                    b.Property<int>("CompanyId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("PosId");

                    b.HasIndex("CompanyId");

                    b.ToTable("Pos");
                });

            modelBuilder.Entity("Facturacion.Domain.Roles", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("RoleID")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("RoleID");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Facturacion.Domain.Taxes", b =>
                {
                    b.Property<int>("TaxId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("TaxId")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Code")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<float>("Value")
                        .HasColumnType("real");

                    b.HasKey("TaxId");

                    b.ToTable("Taxes");
                });

            modelBuilder.Entity("Facturacion.Domain.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CompanyId")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsSuperAdmin")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Facturacion.Domain.VatCondition", b =>
                {
                    b.Property<int>("VatConditionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("VatConditionID")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<int>("CompanyID")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastUpdate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("VatConditionID");

                    b.HasIndex("CompanyID");

                    b.ToTable("VatConditions");
                });

            modelBuilder.Entity("Facturacion.Domain.CompanySettings", b =>
                {
                    b.HasOne("Facturacion.Domain.Company", "Company")
                        .WithOne("CompanySettings")
                        .HasForeignKey("Facturacion.Domain.CompanySettings", "CompanyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Facturacion.Domain.IdentityDocumentType", b =>
                {
                    b.HasOne("Facturacion.Domain.Company", "Company")
                        .WithMany("IdentityDocumentTypes")
                        .HasForeignKey("CompanyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Facturacion.Domain.InvoiceItems", b =>
                {
                    b.HasOne("Facturacion.Domain.Invoices", "Invoice")
                        .WithMany("Items")
                        .HasForeignKey("InvoiceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Facturacion.Domain.Invoices", b =>
                {
                    b.HasOne("Facturacion.Domain.Company", "Company")
                        .WithMany("Invoices")
                        .HasForeignKey("CompanyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Facturacion.Domain.Pos", b =>
                {
                    b.HasOne("Facturacion.Domain.Company", "Company")
                        .WithMany("Pos")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Facturacion.Domain.Users", b =>
                {
                    b.HasOne("Facturacion.Domain.Company", "Company")
                        .WithMany("Users")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Facturacion.Domain.Roles", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Facturacion.Domain.VatCondition", b =>
                {
                    b.HasOne("Facturacion.Domain.Company", "Company")
                        .WithMany("VatConditions")
                        .HasForeignKey("CompanyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
