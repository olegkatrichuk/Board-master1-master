using Microsoft.EntityFrameworkCore.Migrations;

namespace Board.Migrations
{
    public partial class subregion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "States");

            migrationBuilder.DropColumn(
                name: "NameUa",
                table: "States");

            migrationBuilder.AddColumn<string>(
                name: "SubName",
                table: "States",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubNameUa",
                table: "States",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubName",
                table: "States");

            migrationBuilder.DropColumn(
                name: "SubNameUa",
                table: "States");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "States",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameUa",
                table: "States",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
