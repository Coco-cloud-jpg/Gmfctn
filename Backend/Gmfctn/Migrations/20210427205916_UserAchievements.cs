using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gmfctn.Migrations
{
    public partial class UserAchievements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAchievements",
                table: "UserAchievements");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "UserAchievements",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAchievements",
                table: "UserAchievements",
                columns: new[] { "UserId", "AchievementId", "Id" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAchievements",
                table: "UserAchievements");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserAchievements");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAchievements",
                table: "UserAchievements",
                columns: new[] { "UserId", "AchievementId" });
        }
    }
}
