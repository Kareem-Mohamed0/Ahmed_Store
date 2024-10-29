using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AhmedStore.Migrations
{
    /// <inheritdoc />
    public partial class ShopIdInCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShopId",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ShopId",
                table: "Carts",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Shops_ShopId",
                table: "Carts",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Shops_ShopId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_ShopId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "Carts");
        }
    }
}
