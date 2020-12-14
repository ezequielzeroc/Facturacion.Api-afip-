using Microsoft.EntityFrameworkCore.Migrations;

namespace Facturacion.Data.Migrations
{
    public partial class ModPos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pos_Companies_CompanyID",
                table: "Pos");

            migrationBuilder.DropIndex(
                name: "IX_Pos_CompanyID",
                table: "Pos");

            migrationBuilder.DropColumn(
                name: "CompanyID",
                table: "Pos");

            migrationBuilder.CreateIndex(
                name: "IX_Pos_CompanyId",
                table: "Pos",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pos_Companies_CompanyId",
                table: "Pos",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pos_Companies_CompanyId",
                table: "Pos");

            migrationBuilder.DropIndex(
                name: "IX_Pos_CompanyId",
                table: "Pos");

            migrationBuilder.AddColumn<int>(
                name: "CompanyID",
                table: "Pos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pos_CompanyID",
                table: "Pos",
                column: "CompanyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pos_Companies_CompanyID",
                table: "Pos",
                column: "CompanyID",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
