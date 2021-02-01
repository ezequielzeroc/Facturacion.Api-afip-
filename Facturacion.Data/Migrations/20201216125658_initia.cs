using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Facturacion.Data.Migrations
{
    public partial class initia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AfipErrors",
                columns: table => new
                {
                    AfipErrorID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    LastUpdate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AfipErrors", x => x.AfipErrorID);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BusinessName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    DocumentTypeID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypeOfResponsibleID = table.Column<int>(nullable: false),
                    Code = table.Column<int>(nullable: false),
                    Letter = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.DocumentTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Taxes",
                columns: table => new
                {
                    TaxId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Value = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxes", x => x.TaxId);
                });

            migrationBuilder.CreateTable(
                name: "Certificates",
                columns: table => new
                {
                    CertificateID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    IssueDate = table.Column<DateTime>(nullable: true),
                    ValidaUntil = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificates", x => x.CertificateID);
                    table.ForeignKey(
                        name: "FK_Certificates_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanySettings",
                columns: table => new
                {
                    CompanySettingsID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Cuit = table.Column<long>(nullable: false),
                    BusinessName = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    FiscalAddress = table.Column<string>(nullable: true),
                    GrossIncomeNumber = table.Column<string>(nullable: true),
                    StartActivities = table.Column<DateTime>(nullable: false),
                    TaxCategoryId = table.Column<int>(nullable: false),
                    DocumentLogo = table.Column<string>(nullable: true),
                    CompanyID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanySettings", x => x.CompanySettingsID);
                    table.ForeignKey(
                        name: "FK_CompanySettings_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityDocumentTypes",
                columns: table => new
                {
                    IdentityDocumentTypeID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyID = table.Column<int>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    LastUpdate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityDocumentTypes", x => x.IdentityDocumentTypeID);
                    table.ForeignKey(
                        name: "FK_IdentityDocumentTypes_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pos",
                columns: table => new
                {
                    PosId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    BusinessName = table.Column<string>(nullable: true),
                    CompanyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pos", x => x.PosId);
                    table.ForeignKey(
                        name: "FK_Pos_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VatConditions",
                columns: table => new
                {
                    VatConditionID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyID = table.Column<int>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    LastUpdate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VatConditions", x => x.VatConditionID);
                    table.ForeignKey(
                        name: "FK_VatConditions_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InvoiceNumber = table.Column<int>(nullable: false),
                    ClientID = table.Column<int>(nullable: false),
                    CompanyID = table.Column<int>(nullable: false),
                    StoreID = table.Column<int>(nullable: false),
                    PosCode = table.Column<int>(nullable: false),
                    DocumentTypeCode = table.Column<string>(nullable: true),
                    DocumentTypeShortCode = table.Column<string>(nullable: true),
                    DocumentTypeID = table.Column<int>(nullable: false),
                    Letter = table.Column<string>(nullable: true),
                    ConceptCode = table.Column<string>(nullable: true),
                    VatConditionCode = table.Column<string>(nullable: true),
                    IdentityDocumentTypeCode = table.Column<string>(nullable: true),
                    IdentityDocumentNumber = table.Column<string>(nullable: true),
                    ClientName = table.Column<string>(nullable: true),
                    ClientAddress = table.Column<string>(nullable: true),
                    ClientEmail = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    Subtotal = table.Column<decimal>(nullable: false),
                    TotalTaxes = table.Column<decimal>(nullable: false),
                    TotalDiscount = table.Column<decimal>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    LastUpdate = table.Column<DateTime>(nullable: true),
                    Printed = table.Column<bool>(nullable: false),
                    SentEmail = table.Column<bool>(nullable: false),
                    CAE = table.Column<string>(nullable: true),
                    CAEExpiration = table.Column<DateTime>(nullable: true),
                    ExpirationDay = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceID);
                    table.ForeignKey(
                        name: "FK_Invoices_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_DocumentTypes_DocumentTypeID",
                        column: x => x.DocumentTypeID,
                        principalTable: "DocumentTypes",
                        principalColumn: "DocumentTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    IsBlocked = table.Column<bool>(nullable: false),
                    IsSuperAdmin = table.Column<bool>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BarCodes",
                columns: table => new
                {
                    BarCodeID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InvoiceFK = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarCodes", x => x.BarCodeID);
                    table.ForeignKey(
                        name: "FK_BarCodes_Invoices_InvoiceFK",
                        column: x => x.InvoiceFK,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Downloads",
                columns: table => new
                {
                    DownloadID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InvoiceFK = table.Column<int>(nullable: false),
                    File = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Downloaded = table.Column<DateTime>(nullable: false),
                    Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Downloads", x => x.DownloadID);
                    table.ForeignKey(
                        name: "FK_Downloads_Invoices_InvoiceFK",
                        column: x => x.InvoiceFK,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItems",
                columns: table => new
                {
                    ItemID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InvoiceID = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Qtty = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    TaxId = table.Column<decimal>(nullable: false),
                    TaxPercent = table.Column<decimal>(nullable: false),
                    TaxCalculated = table.Column<decimal>(nullable: false),
                    CalculatedDiscount = table.Column<decimal>(nullable: false),
                    TotalDiscount = table.Column<decimal>(nullable: false),
                    Discount = table.Column<decimal>(nullable: false),
                    SubTotal = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    UnitOfMeasurement = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItems", x => x.ItemID);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Invoices_InvoiceID",
                        column: x => x.InvoiceID,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BarCodes_InvoiceFK",
                table: "BarCodes",
                column: "InvoiceFK",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_CompanyID",
                table: "Certificates",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_CompanySettings_CompanyID",
                table: "CompanySettings",
                column: "CompanyID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Downloads_InvoiceFK",
                table: "Downloads",
                column: "InvoiceFK",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IdentityDocumentTypes_CompanyID",
                table: "IdentityDocumentTypes",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_InvoiceID",
                table: "InvoiceItems",
                column: "InvoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CompanyID",
                table: "Invoices",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_DocumentTypeID",
                table: "Invoices",
                column: "DocumentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Pos_CompanyId",
                table: "Pos",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CompanyId",
                table: "Users",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_VatConditions_CompanyID",
                table: "VatConditions",
                column: "CompanyID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AfipErrors");

            migrationBuilder.DropTable(
                name: "BarCodes");

            migrationBuilder.DropTable(
                name: "Certificates");

            migrationBuilder.DropTable(
                name: "CompanySettings");

            migrationBuilder.DropTable(
                name: "Downloads");

            migrationBuilder.DropTable(
                name: "IdentityDocumentTypes");

            migrationBuilder.DropTable(
                name: "InvoiceItems");

            migrationBuilder.DropTable(
                name: "Pos");

            migrationBuilder.DropTable(
                name: "Taxes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "VatConditions");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "DocumentTypes");
        }
    }
}
