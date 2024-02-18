using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TabMakerApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedFKAnnotation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tabs_AspNetUsers_ApplicationUserId",
                table: "Tabs");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Tabs",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Tabs_ApplicationUserId",
                table: "Tabs",
                newName: "IX_Tabs_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tabs_AspNetUsers_UserId",
                table: "Tabs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tabs_AspNetUsers_UserId",
                table: "Tabs");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Tabs",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Tabs_UserId",
                table: "Tabs",
                newName: "IX_Tabs_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tabs_AspNetUsers_ApplicationUserId",
                table: "Tabs",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
