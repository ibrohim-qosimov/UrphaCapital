using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrphaCapital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class paymentMigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "ClickTransactions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "SignTime",
                table: "ClickTransactions",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "PasswordHash", "Salt" },
                values: new object[] { "7K23swqDdRW8mI9Afs3Xb0HN4Aqz5f/pd1em7ZrItJc=", "e615945c-8d00-43d1-8ba2-62a8dff735b0" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "ClickTransactions",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SignTime",
                table: "ClickTransactions",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "PasswordHash", "Salt" },
                values: new object[] { "aOJMDZUsiOMgYlSL+mbN19lnE+32FcTChy/poRp4+EU=", "a807791d-5a2f-4467-8c64-ace484cae52a" });
        }
    }
}
