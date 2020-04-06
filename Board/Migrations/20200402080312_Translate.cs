using Microsoft.EntityFrameworkCore.Migrations;

namespace Board.Migrations
{
    public partial class Translate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cities",
                table: "Adverts");

            migrationBuilder.AddColumn<int>(
                name: "CitiesId",
                table: "Adverts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Citi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    NameUa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citi", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adverts_CitiesId",
                table: "Adverts",
                column: "CitiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adverts_Citi_CitiesId",
                table: "Adverts",
                column: "CitiesId",
                principalTable: "Citi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adverts_Citi_CitiesId",
                table: "Adverts");

            migrationBuilder.DropTable(
                name: "Citi");

            migrationBuilder.DropIndex(
                name: "IX_Adverts_CitiesId",
                table: "Adverts");

            migrationBuilder.DropColumn(
                name: "CitiesId",
                table: "Adverts");

            migrationBuilder.AddColumn<int>(
                name: "Cities",
                table: "Adverts",
                type: "int",
                nullable: true);
        }
    }
}
