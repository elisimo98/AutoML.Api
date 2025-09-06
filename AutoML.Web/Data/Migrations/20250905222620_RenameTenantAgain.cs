using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoML.Web.Migrations
{
    /// <inheritdoc />
    public partial class RenameTenantAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamUsers_Teams_TeamId",
                table: "TeamUsers");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "TeamUsers",
                newName: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamUsers_Teams_TenantId",
                table: "TeamUsers",
                column: "TenantId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamUsers_Teams_TenantId",
                table: "TeamUsers");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "TeamUsers",
                newName: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamUsers_Teams_TeamId",
                table: "TeamUsers",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
