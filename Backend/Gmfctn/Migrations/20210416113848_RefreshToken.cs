using Microsoft.EntityFrameworkCore.Migrations;

namespace Gmfctn.Migrations
{
    public partial class RefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Invalidated",
                table: "RTokens");

            migrationBuilder.DropColumn(
                name: "JwtId",
                table: "RTokens");

            migrationBuilder.DropColumn(
                name: "Used",
                table: "RTokens");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Invalidated",
                table: "RTokens",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "JwtId",
                table: "RTokens",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Used",
                table: "RTokens",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
