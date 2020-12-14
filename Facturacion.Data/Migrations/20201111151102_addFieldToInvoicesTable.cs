using Microsoft.EntityFrameworkCore.Migrations;

namespace Facturacion.Data.Migrations
{
    public partial class addFieldToInvoicesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientAddress",
                table: "Invoices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientEmail",
                table: "Invoices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientName",
                table: "Invoices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConceptCode",
                table: "Invoices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityDocumentCode",
                table: "Invoices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityDocumentNumber",
                table: "Invoices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceTypeCode",
                table: "Invoices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PosCoide",
                table: "Invoices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VatConditionCode",
                table: "Invoices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientAddress",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ClientEmail",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ClientName",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ConceptCode",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "IdentityDocumentCode",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "IdentityDocumentNumber",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "InvoiceTypeCode",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "PosCoide",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "VatConditionCode",
                table: "Invoices");
        }
    }
}
