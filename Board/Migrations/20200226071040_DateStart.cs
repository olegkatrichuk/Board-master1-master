using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Board.Migrations
{
    public partial class DateStart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateStartTime",
                table: "Adverts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateStartTime",
                table: "Adverts");
        }
    }
}
