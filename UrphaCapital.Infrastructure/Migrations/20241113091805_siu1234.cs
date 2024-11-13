using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrphaCapital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class siu1234 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "PasswordHash", "Salt" },
                values: new object[] { "8PL3YumMhsRikd1+srFtEEKMW5FUSOO/xa5+PNm6kGE=", "1422fb08-4403-4b64-87c3-c4c1f17e3dc5" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "PasswordHash", "Salt" },
                values: new object[] { "8vrUD0+k5jLEuuaG3byKRFGnHN8jA2Sz1bl9CcDi0sQ=", "45d1c088-d7b6-48a6-a7de-261eb8de0a13" });
        }
    }
}
