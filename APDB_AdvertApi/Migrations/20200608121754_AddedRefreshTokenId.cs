using Microsoft.EntityFrameworkCore.Migrations;

namespace APDB_AdvertApi.Migrations
{
    public partial class AddedRefreshTokenId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RefreshToken",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RefreshToken");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken",
                column: "Token");
        }
    }
}
