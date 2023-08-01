using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Sql.Migrations
{
    /// <inheritdoc />
    public partial class AddSecondGenres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameGameCategory1",
                columns: table => new
                {
                    SecondaryGamesId = table.Column<int>(type: "int", nullable: false),
                    SecondaryGenresId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameGameCategory1", x => new { x.SecondaryGamesId, x.SecondaryGenresId });
                    table.ForeignKey(
                        name: "FK_GameGameCategory1_GameCategories_SecondaryGenresId",
                        column: x => x.SecondaryGenresId,
                        principalTable: "GameCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameGameCategory1_Games_SecondaryGamesId",
                        column: x => x.SecondaryGamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameGameCategory1_SecondaryGenresId",
                table: "GameGameCategory1",
                column: "SecondaryGenresId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameGameCategory1");
        }
    }
}
