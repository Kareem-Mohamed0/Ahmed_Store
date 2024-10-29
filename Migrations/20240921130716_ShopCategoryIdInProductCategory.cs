using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AhmedStore.Migrations
{
    /// <inheritdoc />
    public partial class ShopCategoryIdInProductCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "DisplayUsersVM",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ShopCategoryId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ShopCategoryId",
                table: "Categories",
                column: "ShopCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_ShopCategories_ShopCategoryId",
                table: "Categories",
                column: "ShopCategoryId",
                principalTable: "ShopCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_ShopCategories_ShopCategoryId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ShopCategoryId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DisplayUsersVM");

            migrationBuilder.DropColumn(
                name: "ShopCategoryId",
                table: "Categories");
        }
    }
}
