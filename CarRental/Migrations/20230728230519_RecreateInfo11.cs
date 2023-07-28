using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Migrations
{
    /// <inheritdoc />
    public partial class RecreateInfo11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarEntityRentalEntity");

            migrationBuilder.AddColumn<int>(
                name: "CarsCarId",
                table: "RentalInfo",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "RentalInfo",
                keyColumn: "RentalId",
                keyValue: 1,
                column: "CarsCarId",
                value: null);

            migrationBuilder.UpdateData(
                table: "RentalInfo",
                keyColumn: "RentalId",
                keyValue: 2,
                column: "CarsCarId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_RentalInfo_CarsCarId",
                table: "RentalInfo",
                column: "CarsCarId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalInfo_CarsInfo_CarsCarId",
                table: "RentalInfo",
                column: "CarsCarId",
                principalTable: "CarsInfo",
                principalColumn: "CarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalInfo_CarsInfo_CarsCarId",
                table: "RentalInfo");

            migrationBuilder.DropIndex(
                name: "IX_RentalInfo_CarsCarId",
                table: "RentalInfo");

            migrationBuilder.DropColumn(
                name: "CarsCarId",
                table: "RentalInfo");

            migrationBuilder.CreateTable(
                name: "CarEntityRentalEntity",
                columns: table => new
                {
                    CarsCarId = table.Column<int>(type: "integer", nullable: false),
                    RentalInfoRentalId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarEntityRentalEntity", x => new { x.CarsCarId, x.RentalInfoRentalId });
                    table.ForeignKey(
                        name: "FK_CarEntityRentalEntity_CarsInfo_CarsCarId",
                        column: x => x.CarsCarId,
                        principalTable: "CarsInfo",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarEntityRentalEntity_RentalInfo_RentalInfoRentalId",
                        column: x => x.RentalInfoRentalId,
                        principalTable: "RentalInfo",
                        principalColumn: "RentalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarEntityRentalEntity_RentalInfoRentalId",
                table: "CarEntityRentalEntity",
                column: "RentalInfoRentalId");
        }
    }
}
