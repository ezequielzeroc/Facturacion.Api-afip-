using Microsoft.EntityFrameworkCore.Migrations;

namespace Facturacion.Data.Migrations
{
    public partial class addfieldpayedf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Payed",
                table: "Invoices",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Payed",
                table: "Invoices");
        }
    }
}
