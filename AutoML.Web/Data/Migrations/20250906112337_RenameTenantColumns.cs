using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoML.Web.Migrations
{
    /// <inheritdoc />
    public partial class RenameTenantColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Tenants_TeamId",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "Projects",
                newName: "TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_TeamId",
                table: "Projects",
                newName: "IX_Projects_TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Tenants_TenantId",
                table: "Projects",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Tenants_TenantId",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "Projects",
                newName: "TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_TenantId",
                table: "Projects",
                newName: "IX_Projects_TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Tenants_TeamId",
                table: "Projects",
                column: "TeamId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
