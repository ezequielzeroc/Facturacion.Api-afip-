using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Facturacion.Data.Migrations
{
    public partial class toSend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentToSend",
                columns: table => new
                {
                    DocumentToSendID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    ToName = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    To = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    AttachInvoice = table.Column<bool>(nullable: false),
                    PriorityLevel = table.Column<int>(nullable: false),
                    TimesSent = table.Column<int>(nullable: false),
                    Sent = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentToSend", x => x.DocumentToSendID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentToSend");
        }
    }
}
