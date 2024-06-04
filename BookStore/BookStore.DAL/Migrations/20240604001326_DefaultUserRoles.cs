using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace _3.BookStore.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DefaultUserRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "58ff34ee-ed69-4d43-9124-53ac00c07c85", null, "Admin", "ADMIN" },
                    { "af3c4ceb-2c5f-4b07-abcc-e2cd010de7f5", null, "Moderator", "MODERATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "58ff34ee-ed69-4d43-9124-53ac00c07c85");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "af3c4ceb-2c5f-4b07-abcc-e2cd010de7f5");
        }
    }
}
