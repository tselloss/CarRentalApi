using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Migrations
{
    /// <inheritdoc />
    public partial class a6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreferenceEntity_UserInfo_ClientUserId",
                table: "PreferenceEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PreferenceEntity",
                table: "PreferenceEntity");

            migrationBuilder.RenameTable(
                name: "PreferenceEntity",
                newName: "PreferenceInfo");

            migrationBuilder.RenameIndex(
                name: "IX_PreferenceEntity_ClientUserId",
                table: "PreferenceInfo",
                newName: "IX_PreferenceInfo_ClientUserId");

            migrationBuilder.AlterColumn<int>(
                name: "ClientUserId",
                table: "PreferenceInfo",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PreferenceInfo",
                table: "PreferenceInfo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PreferenceInfo_UserInfo_ClientUserId",
                table: "PreferenceInfo",
                column: "ClientUserId",
                principalTable: "UserInfo",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreferenceInfo_UserInfo_ClientUserId",
                table: "PreferenceInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PreferenceInfo",
                table: "PreferenceInfo");

            migrationBuilder.RenameTable(
                name: "PreferenceInfo",
                newName: "PreferenceEntity");

            migrationBuilder.RenameIndex(
                name: "IX_PreferenceInfo_ClientUserId",
                table: "PreferenceEntity",
                newName: "IX_PreferenceEntity_ClientUserId");

            migrationBuilder.AlterColumn<int>(
                name: "ClientUserId",
                table: "PreferenceEntity",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PreferenceEntity",
                table: "PreferenceEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PreferenceEntity_UserInfo_ClientUserId",
                table: "PreferenceEntity",
                column: "ClientUserId",
                principalTable: "UserInfo",
                principalColumn: "UserId");
        }
    }
}
