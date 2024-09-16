using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrphaCapital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class toaster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<List<string>>(
                name: "CourseIds",
                table: "Students",
                type: "text[]",
                nullable: true,
                oldClrType: typeof(List<string>),
                oldType: "text[]");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "PasswordHash", "Salt" },
                values: new object[] { "njmXE2qt17lt9DCaV7m5cXLNyd07lyJqQbB5VeTfMfU=", "7b9a34e5-5d54-4c04-945e-c101ac484bf2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<List<string>>(
                name: "CourseIds",
                table: "Students",
                type: "text[]",
                nullable: false,
                oldClrType: typeof(List<string>),
                oldType: "text[]",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "PasswordHash", "Salt" },
                values: new object[] { "ic/h0fbizeHxmzCmNW6s5EDmZD80+w93Y11bQmvCFe8=", "21c9622c-5f95-449e-b5a0-e32e9a8fd3b1" });
        }
    }
}
