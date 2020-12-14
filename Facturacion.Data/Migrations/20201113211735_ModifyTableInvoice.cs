using Microsoft.EntityFrameworkCore.Migrations;

namespace Facturacion.Data.Migrations
{
    public partial class ModifyTableInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PosCoide",
                table: "Invoices");

            migrationBuilder.AddColumn<string>(
                name: "PosCode",
                table: "Invoices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PosCode",
                table: "Invoices");

            migrationBuilder.AddColumn<string>(
                name: "PosCoide",
                table: "Invoices",
                type: "text",
                nullable: true);
        }
    }
}
