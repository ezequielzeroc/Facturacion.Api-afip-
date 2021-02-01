using Microsoft.EntityFrameworkCore.Migrations;

namespace Facturacion.Data.Migrations
{
    public partial class removeField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FinancialMovements_TypeID",
                table: "FinancialMovements");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialMovements_TypeID",
                table: "FinancialMovements",
                column: "TypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FinancialMovements_TypeID",
                table: "FinancialMovements");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialMovements_TypeID",
                table: "FinancialMovements",
                column: "TypeID",
                unique: true);
        }
    }
}
