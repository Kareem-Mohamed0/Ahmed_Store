using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AhmedStore.Migrations
{
    /// <inheritdoc />
    public partial class StrShopCategoryAndShopInCategory : Migration
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

            migrationBuilder.AlterColumn<int>(
                name: "ShopCategoryId",
                table: "Categories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ShopId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ShopId",
                table: "Categories",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_ShopCategories_ShopCategoryId",
                table: "Categories",
                column: "ShopCategoryId",
                principalTable: "ShopCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Shops_ShopId",
                table: "Categories",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_ShopCategories_ShopCategoryId",
                table: "Shops",
                column: "ShopCategoryId",
                principalTable: "ShopCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_ShopCategories_ShopCategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Shops_ShopId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Shops_ShopCategories_ShopCategoryId",
                table: "Shops");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ShopId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ShopCategory",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "ShopId",
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

            migrationBuilder.AlterColumn<int>(
                name: "ShopCategoryId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_ShopCategories_ShopCategoryId",
                table: "Categories",
                column: "ShopCategoryId",
                principalTable: "ShopCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_ShopCategories_ShopCategoryId",
                table: "Shops",
                column: "ShopCategoryId",
                principalTable: "ShopCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
