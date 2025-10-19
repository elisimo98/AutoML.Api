using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoML.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelConfigEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModelConfig_Tenants_TenantId",
                table: "ModelConfig");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ModelConfig",
                newName: "TargetColumn");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "ModelConfig",
                newName: "ModelType");

            migrationBuilder.RenameColumn(
                name: "ConfigJson",
                table: "ModelConfig",
                newName: "FileName");

            migrationBuilder.AlterColumn<string>(
                name: "ExternalId",
                table: "Tenants",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "TenantId",
                table: "ModelConfig",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<int>(
                name: "Epochs",
                table: "ModelConfig",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RandomState",
                table: "ModelConfig",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "TestSize",
                table: "ModelConfig",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Tenants_ExternalId",
                table: "Tenants",
                column: "ExternalId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_ExternalId",
                table: "Tenants",
                column: "ExternalId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ModelConfig_Tenants_TenantId",
                table: "ModelConfig",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "ExternalId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModelConfig_Tenants_TenantId",
                table: "ModelConfig");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Tenants_ExternalId",
                table: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_Tenants_ExternalId",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Epochs",
                table: "ModelConfig");

            migrationBuilder.DropColumn(
                name: "RandomState",
                table: "ModelConfig");

            migrationBuilder.DropColumn(
                name: "TestSize",
                table: "ModelConfig");

            migrationBuilder.RenameColumn(
                name: "TargetColumn",
                table: "ModelConfig",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ModelType",
                table: "ModelConfig",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "ModelConfig",
                newName: "ConfigJson");

            migrationBuilder.AlterColumn<string>(
                name: "ExternalId",
                table: "Tenants",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<long>(
                name: "TenantId",
                table: "ModelConfig",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_ModelConfig_Tenants_TenantId",
                table: "ModelConfig",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
