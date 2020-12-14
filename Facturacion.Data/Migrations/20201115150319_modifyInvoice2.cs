using Microsoft.EntityFrameworkCore.Migrations;

namespace Facturacion.Data.Migrations
{
    public partial class modifyInvoice2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceTypeCode",
                table: "Invoices");

            migrationBuilder.AddColumn<string>(
                name: "DocumentTypeCode",
                table: "Invoices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Letter",
                table: "Invoices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentTypeCode",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Letter",
                table: "Invoices");

            migrationBuilder.AddColumn<string>(
                name: "InvoiceTypeCode",
                table: "Invoices",
                type: "text",
                nullable: true);
        }
    }
}
