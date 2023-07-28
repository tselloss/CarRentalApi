using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Migrations
{
    /// <inheritdoc />
    public partial class RecreateInfo5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "RentalInfo",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarId",
                table: "RentalInfo");
        }
    }
}
