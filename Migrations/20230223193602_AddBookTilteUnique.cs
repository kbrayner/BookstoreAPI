using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookstoreSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddBookTilteUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Books_Title",
                table: "Books",
                column: "Title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Books_Title",
                table: "Books");
        }
    }
}
