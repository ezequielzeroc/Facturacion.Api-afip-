using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Facturacion.Data.Migrations
{
    public partial class addfieldInvoiceDAte : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "InvoiceDate",
                table: "Invoices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceDate",
                table: "Invoices");
        }
    }
}
