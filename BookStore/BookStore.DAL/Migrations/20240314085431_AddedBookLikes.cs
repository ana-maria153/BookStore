using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3.BookStore.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedBookLikes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookLikes",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookLikes",
                table: "Books");
        }
    }
}
