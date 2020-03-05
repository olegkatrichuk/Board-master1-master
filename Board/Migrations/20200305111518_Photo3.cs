using Microsoft.EntityFrameworkCore.Migrations;

namespace Board.Migrations
{
    public partial class Photo3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "AdvertPhotos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "AdvertPhotos");
        }
    }
}
