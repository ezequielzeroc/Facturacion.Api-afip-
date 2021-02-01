using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Facturacion.Data.Migrations
{
    public partial class financialMovements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinancialMovementTypes",
                columns: table => new
                {
                    MovementTypeID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialMovementTypes", x => x.MovementTypeID);
                });

            migrationBuilder.CreateTable(
                name: "FinancialMovements",
                columns: table => new
                {
                    MovementID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyID = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Ammount = table.Column<decimal>(nullable: false),
                    TypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialMovements", x => x.MovementID);
                    table.ForeignKey(
                        name: "FK_FinancialMovements_FinancialMovementTypes_TypeID",
                        column: x => x.TypeID,
                        principalTable: "FinancialMovementTypes",
                        principalColumn: "MovementTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinancialMovements_TypeID",
                table: "FinancialMovements",
                column: "TypeID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinancialMovements");

            migrationBuilder.DropTable(
                name: "FinancialMovementTypes");
        }
    }
}
