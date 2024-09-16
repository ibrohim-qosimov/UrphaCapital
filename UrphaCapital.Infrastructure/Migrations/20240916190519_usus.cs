using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UrphaCapital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class usus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Paymentss",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StudentId = table.Column<long>(type: "bigint", nullable: false),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentStatus = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paymentss", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Paymentss_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Paymentss_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "PasswordHash", "Salt" },
                values: new object[] { "LXixSmGK1zCimS7HJigEQYR1MWACNvNMb9ARcmRusdU=", "c8dc5061-7022-41ee-b2aa-d397f56b3fb3" });

            migrationBuilder.CreateIndex(
                name: "IX_Paymentss_CourseId",
                table: "Paymentss",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Paymentss_StudentId",
                table: "Paymentss",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Paymentss");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "PasswordHash", "Salt" },
                values: new object[] { "njmXE2qt17lt9DCaV7m5cXLNyd07lyJqQbB5VeTfMfU=", "7b9a34e5-5d54-4c04-945e-c101ac484bf2" });
        }
    }
}
