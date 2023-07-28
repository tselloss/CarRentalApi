using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Migrations
{
    /// <inheritdoc />
    public partial class RecreateInfo7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarId",
                table: "RentalInfo");

            migrationBuilder.DropColumn(
                name: "RentalId",
                table: "CarsInfo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "RentalInfo",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RentalId",
                table: "CarsInfo",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "CarsInfo",
                keyColumn: "CarId",
                keyValue: 1,
                column: "RentalId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "CarsInfo",
                keyColumn: "CarId",
                keyValue: 2,
                column: "RentalId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "CarsInfo",
                keyColumn: "CarId",
                keyValue: 3,
                column: "RentalId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "RentalInfo",
                keyColumn: "RentalId",
                keyValue: 1,
                column: "CarId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "RentalInfo",
                keyColumn: "RentalId",
                keyValue: 2,
                column: "CarId",
                value: 0);
        }
    }
}
