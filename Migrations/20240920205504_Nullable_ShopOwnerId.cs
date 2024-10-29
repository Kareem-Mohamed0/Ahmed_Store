using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AhmedStore.Migrations
{
    /// <inheritdoc />
    public partial class Nullable_ShopOwnerId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shops_AspNetUsers_ShopOwnerId",
                table: "Shops");

            migrationBuilder.AlterColumn<string>(
                name: "ShopOwnerId",
                table: "Shops",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_AspNetUsers_ShopOwnerId",
                table: "Shops",
                column: "ShopOwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shops_AspNetUsers_ShopOwnerId",
                table: "Shops");

            migrationBuilder.AlterColumn<string>(
                name: "ShopOwnerId",
                table: "Shops",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_AspNetUsers_ShopOwnerId",
                table: "Shops",
                column: "ShopOwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
