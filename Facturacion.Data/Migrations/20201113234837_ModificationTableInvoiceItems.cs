using Microsoft.EntityFrameworkCore.Migrations;

namespace Facturacion.Data.Migrations
{
    public partial class ModificationTableInvoiceItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalculatedTax",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "DiscountPercent",
                table: "InvoiceItems");

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "InvoiceItems",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TaxCalculated",
                table: "InvoiceItems",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalDiscount",
                table: "InvoiceItems",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "TaxCalculated",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "TotalDiscount",
                table: "InvoiceItems");

            migrationBuilder.AddColumn<decimal>(
                name: "CalculatedTax",
                table: "InvoiceItems",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercent",
                table: "InvoiceItems",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
