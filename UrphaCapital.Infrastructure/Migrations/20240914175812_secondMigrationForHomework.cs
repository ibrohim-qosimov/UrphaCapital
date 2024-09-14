using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrphaCapital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class secondMigrationForHomework : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "studentId",
                table: "Homeworks",
                newName: "StudentId");

            migrationBuilder.AddColumn<int>(
                name: "Grade",
                table: "Homeworks",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "MentorId",
                table: "Homeworks",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "PasswordHash", "Salt" },
                values: new object[] { "4Il9e3rB5x4yv8ICacJKVcQIw7AM0ckPme6Io4ttYsg=", "8d09c77e-61d4-45c6-b403-a1bba01d1946" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Homeworks");

            migrationBuilder.DropColumn(
                name: "MentorId",
                table: "Homeworks");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Homeworks",
                newName: "studentId");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "PasswordHash", "Salt" },
                values: new object[] { "0uWF1h1dUY3IskvUlLOklhBlgmBACiFQQ/zcLXz1VFU=", "82f73fc9-42fe-417f-afae-7dcbbfd629fb" });
        }
    }
}
