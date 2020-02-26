using Microsoft.EntityFrameworkCore.Migrations;

namespace Board.Migrations
{
    public partial class City : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Adverts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Adverts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Adverts");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Adverts");
        }
    }
}
