using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TabMakerApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdjustedFKs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tabs_AspNetUsers_ApplicationUserId",
                table: "Tabs");

            migrationBuilder.DropIndex(
                name: "IX_Tabs_ApplicationUserId",
                table: "Tabs");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Tabs");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Tabs",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Tabs_UserId",
                table: "Tabs",
                column: "UserId");

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

            migrationBuilder.DropIndex(
                name: "IX_Tabs_UserId",
                table: "Tabs");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Tabs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Tabs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tabs_ApplicationUserId",
                table: "Tabs",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tabs_AspNetUsers_ApplicationUserId",
                table: "Tabs",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
