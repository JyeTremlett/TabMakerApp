using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TabMakerApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class CustomUserData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tab",
                table: "Tab");

            migrationBuilder.RenameTable(
                name: "Tab",
                newName: "Tabs");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Tabs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Tabs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tabs",
                table: "Tabs",
                column: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tabs_AspNetUsers_ApplicationUserId",
                table: "Tabs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tabs",
                table: "Tabs");

            migrationBuilder.DropIndex(
                name: "IX_Tabs_ApplicationUserId",
                table: "Tabs");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Tabs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tabs");

            migrationBuilder.RenameTable(
                name: "Tabs",
                newName: "Tab");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tab",
                table: "Tab",
                column: "Id");
        }
    }
}
