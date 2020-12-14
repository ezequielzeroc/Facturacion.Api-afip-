using Microsoft.EntityFrameworkCore.Migrations;

namespace Facturacion.Data.Migrations
{
    public partial class modifyInvoice3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Invoices_DocumentTypeID",
                table: "Invoices",
                column: "DocumentTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_DocumentTypes_DocumentTypeID",
                table: "Invoices",
                column: "DocumentTypeID",
                principalTable: "DocumentTypes",
                principalColumn: "DocumentTypeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_DocumentTypes_DocumentTypeID",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_DocumentTypeID",
                table: "Invoices");
        }
    }
}
