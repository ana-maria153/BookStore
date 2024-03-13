using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3.BookStore.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedBookPublicID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PublicID",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicID",
                table: "Books");
        }
    }
}
