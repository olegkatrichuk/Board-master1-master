using Microsoft.EntityFrameworkCore.Migrations;

namespace Board.Migrations
{
    public partial class Photo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "Adverts");

            migrationBuilder.AddColumn<int>(
                name: "Cities",
                table: "Adverts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phones",
                table: "Adverts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cities",
                table: "Adverts");

            migrationBuilder.DropColumn(
                name: "Phones",
                table: "Adverts");

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "Adverts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
