using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Migrations
{
    /// <inheritdoc />
    public partial class aaa2a211 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarsInfo_RentalInfo_RentalId",
                table: "CarsInfo");

            migrationBuilder.DropIndex(
                name: "IX_CarsInfo_RentalId",
                table: "CarsInfo");

            migrationBuilder.DropColumn(
                name: "RentalId",
                table: "CarsInfo");

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "RentalInfo",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RentalInfo_CarId",
                table: "RentalInfo",
                column: "CarId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalInfo_CarsInfo_CarId",
                table: "RentalInfo",
                column: "CarId",
                principalTable: "CarsInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalInfo_CarsInfo_CarId",
                table: "RentalInfo");

            migrationBuilder.DropIndex(
                name: "IX_RentalInfo_CarId",
                table: "RentalInfo");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "RentalInfo");

            migrationBuilder.AddColumn<string>(
                name: "RentalId",
                table: "CarsInfo",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarsInfo_RentalId",
                table: "CarsInfo",
                column: "RentalId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarsInfo_RentalInfo_RentalId",
                table: "CarsInfo",
                column: "RentalId",
                principalTable: "RentalInfo",
                principalColumn: "Id");
        }
    }
}
