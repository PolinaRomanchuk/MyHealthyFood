using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Sql.Migrations
{
    /// <inheritdoc />
    public partial class AddManufacturer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Manufacturer",
                table: "StoreItems");

            migrationBuilder.AddColumn<int>(
                name: "ManufacturerId",
                table: "StoreItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Manufacturer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturer", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreItems_ManufacturerId",
                table: "StoreItems",
                column: "ManufacturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreItems_Manufacturer_ManufacturerId",
                table: "StoreItems",
                column: "ManufacturerId",
                principalTable: "Manufacturer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreItems_Manufacturer_ManufacturerId",
                table: "StoreItems");

            migrationBuilder.DropTable(
                name: "Manufacturer");

            migrationBuilder.DropIndex(
                name: "IX_StoreItems_ManufacturerId",
                table: "StoreItems");

            migrationBuilder.DropColumn(
                name: "ManufacturerId",
                table: "StoreItems");

            migrationBuilder.AddColumn<string>(
                name: "Manufacturer",
                table: "StoreItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
