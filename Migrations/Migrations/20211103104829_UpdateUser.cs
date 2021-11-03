using Microsoft.EntityFrameworkCore.Migrations;

namespace Migrations.Migrations
{
    public partial class UpdateUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SomeValue",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SomeValue1",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SomeValue2",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SomeValue",
                table: "Users",
                type: "text",
                nullable: true,
                defaultValue: "value");

            migrationBuilder.AddColumn<string>(
                name: "SomeValue1",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SomeValue2",
                table: "Users",
                type: "text",
                nullable: true);
        }
    }
}
