using Microsoft.EntityFrameworkCore.Migrations;

namespace Board.Migrations
{
    public partial class Photo2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdvertPhoto_Adverts_AdvertId",
                table: "AdvertPhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdvertPhoto",
                table: "AdvertPhoto");

            migrationBuilder.RenameTable(
                name: "AdvertPhoto",
                newName: "AdvertPhotos");

            migrationBuilder.RenameIndex(
                name: "IX_AdvertPhoto_AdvertId",
                table: "AdvertPhotos",
                newName: "IX_AdvertPhotos_AdvertId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdvertPhotos",
                table: "AdvertPhotos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertPhotos_Adverts_AdvertId",
                table: "AdvertPhotos",
                column: "AdvertId",
                principalTable: "Adverts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdvertPhotos_Adverts_AdvertId",
                table: "AdvertPhotos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdvertPhotos",
                table: "AdvertPhotos");

            migrationBuilder.RenameTable(
                name: "AdvertPhotos",
                newName: "AdvertPhoto");

            migrationBuilder.RenameIndex(
                name: "IX_AdvertPhotos_AdvertId",
                table: "AdvertPhoto",
                newName: "IX_AdvertPhoto_AdvertId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdvertPhoto",
                table: "AdvertPhoto",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertPhoto_Adverts_AdvertId",
                table: "AdvertPhoto",
                column: "AdvertId",
                principalTable: "Adverts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
