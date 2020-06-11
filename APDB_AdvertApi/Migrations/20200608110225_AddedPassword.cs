using Microsoft.EntityFrameworkCore.Migrations;

namespace APDB_AdvertApi.Migrations
{
    public partial class AddedPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Client",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Client");
        }
    }
}
