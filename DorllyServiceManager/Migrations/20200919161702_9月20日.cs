using Microsoft.EntityFrameworkCore.Migrations;

namespace DorllyServiceManager.Migrations
{
    public partial class _9月20日 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Service",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PropertyValue",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "PropertyValue",
                maxLength: 24,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(24)",
                oldMaxLength: 24,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PropertyId",
                table: "PropertyValue",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Role_Code",
                table: "Role",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PropertyValue_Code",
                table: "PropertyValue",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PropertyValue_PropertyId",
                table: "PropertyValue",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyValue_ServiceProperty_PropertyId",
                table: "PropertyValue",
                column: "PropertyId",
                principalTable: "ServiceProperty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyValue_ServiceProperty_PropertyId",
                table: "PropertyValue");

            migrationBuilder.DropIndex(
                name: "IX_Role_Code",
                table: "Role");

            migrationBuilder.DropIndex(
                name: "IX_PropertyValue_Code",
                table: "PropertyValue");

            migrationBuilder.DropIndex(
                name: "IX_PropertyValue_PropertyId",
                table: "PropertyValue");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "PropertyValue");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Service",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PropertyValue",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "PropertyValue",
                type: "nvarchar(24)",
                maxLength: 24,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 24);
        }
    }
}
