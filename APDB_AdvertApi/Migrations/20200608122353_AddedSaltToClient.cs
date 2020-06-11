using Microsoft.EntityFrameworkCore.Migrations;

namespace APDB_AdvertApi.Migrations
{
    public partial class AddedSaltToClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "Client",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Client");
        }
    }
}
