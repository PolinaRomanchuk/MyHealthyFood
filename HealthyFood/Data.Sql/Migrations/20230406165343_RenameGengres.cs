using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Sql.Migrations
{
    /// <inheritdoc />
    public partial class RenameGengres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameGameCategory_GameCategories_GenreId",
                table: "GameGameCategory");

            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "GameGameCategory",
                newName: "GenresId");

            migrationBuilder.RenameIndex(
                name: "IX_GameGameCategory_GenreId",
                table: "GameGameCategory",
                newName: "IX_GameGameCategory_GenresId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameGameCategory_GameCategories_GenresId",
                table: "GameGameCategory",
                column: "GenresId",
                principalTable: "GameCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameGameCategory_GameCategories_GenresId",
                table: "GameGameCategory");

            migrationBuilder.RenameColumn(
                name: "GenresId",
                table: "GameGameCategory",
                newName: "GenreId");

            migrationBuilder.RenameIndex(
                name: "IX_GameGameCategory_GenresId",
                table: "GameGameCategory",
                newName: "IX_GameGameCategory_GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameGameCategory_GameCategories_GenreId",
                table: "GameGameCategory",
                column: "GenreId",
                principalTable: "GameCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
