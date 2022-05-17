using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Migrations.Migrations
{
    public partial class TimesPan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Temp",
                columns: table => new
                {
                    Time = table.Column<TimeSpan>(type: "interval", nullable: false, defaultValue: new TimeSpan(0, 0, 0, 0, 0))
                },
                constraints: table =>
                {
                });

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: "masterId",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAECcmRWUVVXjvAc3c/wXEINknZstQREewkxe4lDE8/seSl3XM8tB+ymo2cfIrHUOJOQ==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Temp");

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: "masterId",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEBO0w2+iT6KVJBO/m2yQWrsULIa/00eWLLDX9Jr1ENr4oJwnNZ8l6P/z+s0nGKNl9A==");
        }
    }
}
