using Microsoft.EntityFrameworkCore.Migrations;

namespace Migrations.Migrations
{
    public partial class Add_default_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "Login", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "masterId", 0, "v3261te0-g279-8c3q-b8ii-ss9s44m894v207", "admin@master.com", true, null, null, false, null, null, "ADMIN@MASTER.COM", "ADMIN@MASTER.COM", null, "AQAAAAEAACcQAAAAEBO0w2+iT6KVJBO/m2yQWrsULIa/00eWLLDX9Jr1ENr4oJwnNZ8l6P/z+s0nGKNl9A==", null, false, "v3261te0-g279-8c3q-b8ii-ss9s44m894v207", false, "admin@master.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: "masterId");
        }
    }
}
