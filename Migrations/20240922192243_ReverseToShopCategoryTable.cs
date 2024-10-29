using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AhmedStore.Migrations
{
    /// <inheritdoc />
    public partial class ReverseToShopCategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_ShopCategories_ShopCategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Shops_ShopCategories_ShopCategoryId",
                table: "Shops");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ShopCategoryId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ShopCategory",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "ShopCategoryId",
                table: "Categories");

            migrationBuilder.AlterColumn<int>(
                name: "ShopCategoryId",
                table: "Shops",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_ShopCategories_ShopCategoryId",
                table: "Shops",
                column: "ShopCategoryId",
                principalTable: "ShopCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shops_ShopCategories_ShopCategoryId",
                table: "Shops");

            migrationBuilder.AlterColumn<int>(
                name: "ShopCategoryId",
                table: "Shops",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ShopCategory",
                table: "Shops",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ShopCategoryId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ShopCategoryId",
                table: "Categories",
                column: "ShopCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_ShopCategories_ShopCategoryId",
                table: "Categories",
                column: "ShopCategoryId",
                principalTable: "ShopCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_ShopCategories_ShopCategoryId",
                table: "Shops",
                column: "ShopCategoryId",
                principalTable: "ShopCategories",
                principalColumn: "Id");
        }
    }
}
