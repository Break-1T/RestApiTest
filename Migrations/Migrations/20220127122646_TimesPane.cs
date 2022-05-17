using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Migrations.Migrations
{
    public partial class TimesPane : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Temp",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Temp",
                table: "Temp",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: "masterId",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAECMlnmaK/B1tB6AG3r2ZX80re3J1J8lEHMyurD4oapDMqJ/PloVh4C6RGETwhrIHDg==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Temp",
                table: "Temp");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Temp");

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: "masterId",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAECcmRWUVVXjvAc3c/wXEINknZstQREewkxe4lDE8/seSl3XM8tB+ymo2cfIrHUOJOQ==");
        }
    }
}
