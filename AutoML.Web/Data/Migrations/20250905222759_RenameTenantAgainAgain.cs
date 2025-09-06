using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoML.Web.Migrations
{
    /// <inheritdoc />
    public partial class RenameTenantAgainAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Teams_TeamId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamUsers_AspNetUsers_UserId",
                table: "TeamUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamUsers_Teams_TenantId",
                table: "TeamUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamUsers",
                table: "TeamUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teams",
                table: "Teams");

            migrationBuilder.RenameTable(
                name: "TeamUsers",
                newName: "TenantUsers");

            migrationBuilder.RenameTable(
                name: "Teams",
                newName: "Tenants");

            migrationBuilder.RenameIndex(
                name: "IX_TeamUsers_UserId",
                table: "TenantUsers",
                newName: "IX_TenantUsers_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TenantUsers",
                table: "TenantUsers",
                columns: new[] { "TenantId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tenants",
                table: "Tenants",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Tenants_TeamId",
                table: "Projects",
                column: "TeamId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TenantUsers_AspNetUsers_UserId",
                table: "TenantUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TenantUsers_Tenants_TenantId",
                table: "TenantUsers",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Tenants_TeamId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_TenantUsers_AspNetUsers_UserId",
                table: "TenantUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_TenantUsers_Tenants_TenantId",
                table: "TenantUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TenantUsers",
                table: "TenantUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tenants",
                table: "Tenants");

            migrationBuilder.RenameTable(
                name: "TenantUsers",
                newName: "TeamUsers");

            migrationBuilder.RenameTable(
                name: "Tenants",
                newName: "Teams");

            migrationBuilder.RenameIndex(
                name: "IX_TenantUsers_UserId",
                table: "TeamUsers",
                newName: "IX_TeamUsers_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamUsers",
                table: "TeamUsers",
                columns: new[] { "TenantId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teams",
                table: "Teams",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Teams_TeamId",
                table: "Projects",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamUsers_AspNetUsers_UserId",
                table: "TeamUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamUsers_Teams_TenantId",
                table: "TeamUsers",
                column: "TenantId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
