using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UrphaCapital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class siu3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Homeworks_Lessons_LessonId",
                table: "Homeworks");

            migrationBuilder.DropIndex(
                name: "IX_Homeworks_LessonId",
                table: "Homeworks");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Homeworks",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_Homeworks_Lessons_Id",
                table: "Homeworks",
                column: "Id",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Homeworks_Lessons_Id",
                table: "Homeworks");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Homeworks",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_Homeworks_LessonId",
                table: "Homeworks",
                column: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Homeworks_Lessons_LessonId",
                table: "Homeworks",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
