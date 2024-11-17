using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrphaCapital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class helpChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Helps");

            migrationBuilder.DropColumn(
                name: "CourseType",
                table: "Helps");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "PasswordHash", "Salt" },
                values: new object[] { "FJ5hpO8kCykiXVyVXC7F5wdDiu62kLnc2WKPDRz638o=", "8f28e690-ae6b-4aa3-b6cb-19a52ecb3e68" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Helps",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CourseType",
                table: "Helps",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "PasswordHash", "Salt" },
                values: new object[] { "AtdIj/Rv3TlpwyBq/fYvSZEUMg4hRpnTRfIBvLi7Oog=", "b92a3e48-b326-49f7-addb-c2a89640be36" });
        }
    }
}
