using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Migrations
{
    /// <inheritdoc />
    public partial class RecreateInfo2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "RentalInfo",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "RentalInfo",
                keyColumn: "RentalId",
                keyValue: 1,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "RentalInfo",
                keyColumn: "RentalId",
                keyValue: 2,
                column: "UserId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_RentalInfo_UserId",
                table: "RentalInfo",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalInfo_UsersInfo_UserId",
                table: "RentalInfo",
                column: "UserId",
                principalTable: "UsersInfo",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalInfo_UsersInfo_UserId",
                table: "RentalInfo");

            migrationBuilder.DropIndex(
                name: "IX_RentalInfo_UserId",
                table: "RentalInfo");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "RentalInfo");
        }
    }
}
