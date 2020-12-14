using Microsoft.EntityFrameworkCore.Migrations;

namespace Facturacion.Data.Migrations
{
    public partial class addBarCodeTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceID",
                table: "BarCodes");

            migrationBuilder.AddColumn<int>(
                name: "InvoiceFK",
                table: "BarCodes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceFK",
                table: "BarCodes");

            migrationBuilder.AddColumn<int>(
                name: "InvoiceID",
                table: "BarCodes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
