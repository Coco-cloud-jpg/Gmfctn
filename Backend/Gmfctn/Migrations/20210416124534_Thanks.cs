using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gmfctn.Migrations
{
    public partial class Thanks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Thanks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thanks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Thanks_Users_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Thanks_FromUserId",
                table: "Thanks",
                column: "FromUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Thanks");
        }
    }
}
