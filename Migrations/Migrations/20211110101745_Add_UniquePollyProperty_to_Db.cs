using Microsoft.EntityFrameworkCore.Migrations;

namespace Migrations.Migrations
{
    public partial class Add_UniquePollyProperty_to_Db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UniquePollyProperty",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: "masterId",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEINVFJ1BSxM2xCq8zG0EVqTU1Q9Yu+WPGy5DdKyTH+kXcIM+QoWnx0p5547NBOJz9A==");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UniquePollyProperty",
                table: "Users",
                column: "UniquePollyProperty",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_UniquePollyProperty",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UniquePollyProperty",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: "masterId",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEBO0w2+iT6KVJBO/m2yQWrsULIa/00eWLLDX9Jr1ENr4oJwnNZ8l6P/z+s0nGKNl9A==");
        }
    }
}
