using Microsoft.EntityFrameworkCore.Migrations;

namespace Facturacion.Data.Migrations
{
    public partial class addField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InvoiceID",
                table: "FinancialMovements",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FinancialMovements_InvoiceID",
                table: "FinancialMovements",
                column: "InvoiceID");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialMovements_Invoices_InvoiceID",
                table: "FinancialMovements",
                column: "InvoiceID",
                principalTable: "Invoices",
                principalColumn: "InvoiceID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialMovements_Invoices_InvoiceID",
                table: "FinancialMovements");

            migrationBuilder.DropIndex(
                name: "IX_FinancialMovements_InvoiceID",
                table: "FinancialMovements");

            migrationBuilder.DropColumn(
                name: "InvoiceID",
                table: "FinancialMovements");
        }
    }
}
