using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrphaCapital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class test1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MenthorId",
                table: "Courses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "MenthorId",
                table: "Courses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
