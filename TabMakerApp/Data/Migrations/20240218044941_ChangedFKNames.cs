using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TabMakerApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedFKNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tabs_AspNetUsers_ApplicationUserId",
                table: "Tabs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tabs");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Tabs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);


            migrationBuilder.AddForeignKey(
                name: "FK_Tabs_AspNetUsers_ApplicationUserId",
                table: "Tabs",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_Tabs_AspNetUsers_ApplicationUserId",
                table: "Tabs");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Tabs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Tabs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "IdentityUserClaim<string>",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_IdentityUserClaim<string>_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Tabs_AspNetUsers_ApplicationUserId",
                table: "Tabs",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
