using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookSpark.Migrations
{
    /// <inheritdoc />
    public partial class AddWishlist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Wishlist_WishlistId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_WishlistId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WishlistId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Wishlist",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlist_AppUserId",
                table: "Wishlist",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlist_AspNetUsers_AppUserId",
                table: "Wishlist",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishlist_AspNetUsers_AppUserId",
                table: "Wishlist");

            migrationBuilder.DropIndex(
                name: "IX_Wishlist_AppUserId",
                table: "Wishlist");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Wishlist");

            migrationBuilder.AddColumn<string>(
                name: "WishlistId",
                table: "AspNetUsers",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_WishlistId",
                table: "AspNetUsers",
                column: "WishlistId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Wishlist_WishlistId",
                table: "AspNetUsers",
                column: "WishlistId",
                principalTable: "Wishlist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
