using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrphaCapital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Email", "Name", "PasswordHash", "PhoneNumber", "Role", "Salt" },
                values: new object[] { 1L, "admin@gmail.com", "Ozodali", "Admin01!", "+998934013443", "SuperAdmin", "ff24169f-636e-400a-81b1-b700603fa826" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
