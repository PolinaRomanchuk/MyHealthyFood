using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Sql.Migrations
{
    /// <inheritdoc />
    public partial class AddCreaterForGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreaterId",
                table: "Games",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Games_CreaterId",
                table: "Games",
                column: "CreaterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Users_CreaterId",
                table: "Games",
                column: "CreaterId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Users_CreaterId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_CreaterId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "CreaterId",
                table: "Games");
        }
    }
}
