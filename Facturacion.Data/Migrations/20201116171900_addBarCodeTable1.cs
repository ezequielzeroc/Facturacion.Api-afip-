using Microsoft.EntityFrameworkCore.Migrations;

namespace Facturacion.Data.Migrations
{
    public partial class addBarCodeTable1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarCodes_Invoices_InvoiceID",
                table: "BarCodes");

            migrationBuilder.DropIndex(
                name: "IX_BarCodes_InvoiceID",
                table: "BarCodes");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_BarCodes_InvoiceID",
                table: "Invoices",
                column: "InvoiceID",
                principalTable: "BarCodes",
                principalColumn: "BarCodeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_BarCodes_InvoiceID",
                table: "Invoices");

            migrationBuilder.CreateIndex(
                name: "IX_BarCodes_InvoiceID",
                table: "BarCodes",
                column: "InvoiceID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BarCodes_Invoices_InvoiceID",
                table: "BarCodes",
                column: "InvoiceID",
                principalTable: "Invoices",
                principalColumn: "InvoiceID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
