using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrphaCapital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adminSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "PasswordHash", "Salt" },
                values: new object[] { "WNnk4s2M362PsRP5CEEW8rcEZj8oDrzMcAUzj33CV+w=", "e253cb64-696a-4acd-b812-ca1bb32d371b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "PasswordHash", "Salt" },
                values: new object[] { "Admin01!", "b05c0da6-5080-4bfc-80e7-f01917f47849" });
        }
    }
}
