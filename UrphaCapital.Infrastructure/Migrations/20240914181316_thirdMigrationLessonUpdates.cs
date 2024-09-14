using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrphaCapital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class thirdMigrationLessonUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Mentors",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Courses",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Admins",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "HomeworkDescription",
                table: "Lessons",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Name", "PasswordHash", "Salt" },
                values: new object[] { "Ozod Ali", "z7PJWUN+y4J2dW7SdJ82kcVjY3YYG7+8UV1OUkPc+eo=", "a5cae941-dd44-4b5d-bdc0-9882db8ee01c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HomeworkDescription",
                table: "Lessons");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Mentors",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Courses",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Admins",
                newName: "Title");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "PasswordHash", "Salt", "Title" },
                values: new object[] { "4Il9e3rB5x4yv8ICacJKVcQIw7AM0ckPme6Io4ttYsg=", "8d09c77e-61d4-45c6-b403-a1bba01d1946", null });
        }
    }
}
