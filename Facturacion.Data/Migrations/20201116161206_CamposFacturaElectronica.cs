using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Facturacion.Data.Migrations
{
    public partial class CamposFacturaElectronica : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CAE",
                table: "Invoices",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CAEExpiration",
                table: "Invoices",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDay",
                table: "Invoices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CAE",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "CAEExpiration",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ExpirationDay",
                table: "Invoices");
        }
    }
}
