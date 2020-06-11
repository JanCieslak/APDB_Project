using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APDB_AdvertApi.Migrations
{
    public partial class AddedRefreshTokens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Token = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Token);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshToken");
        }
    }
}
