using Microsoft.EntityFrameworkCore.Migrations;

namespace Facturacion.Data.Migrations
{
    public partial class addfieldIsCompleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isCompleted",
                table: "FinancialMovements",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isCompleted",
                table: "FinancialMovements");
        }
    }
}
