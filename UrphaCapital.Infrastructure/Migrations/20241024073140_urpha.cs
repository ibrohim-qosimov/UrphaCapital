using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrphaCapital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class urpha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "PasswordHash", "Salt" },
                values: new object[] { "pzvrqLApZXV+oz7KAG4Y8VkLed88mEh4HRBT88dT7e8=", "46dec9e0-d8e2-423f-9dfa-800e431a5a6f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "PasswordHash", "Salt" },
                values: new object[] { "hqrDexSpYly+KPaMHcBXxT7Y+gcF0/kO40y2K6ijUJQ=", "55e58b87-807b-4b8e-a581-4d76e93efe33" });
        }
    }
}
