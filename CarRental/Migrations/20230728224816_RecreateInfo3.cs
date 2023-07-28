using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Migrations
{
    /// <inheritdoc />
    public partial class RecreateInfo3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarEntityRentalEntity",
                columns: table => new
                {
                    CarsInfoCarId = table.Column<int>(type: "integer", nullable: false),
                    RentalInfoRentalId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarEntityRentalEntity", x => new { x.CarsInfoCarId, x.RentalInfoRentalId });
                    table.ForeignKey(
                        name: "FK_CarEntityRentalEntity_CarsInfo_CarsInfoCarId",
                        column: x => x.CarsInfoCarId,
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarEntityRentalEntity");
        }
    }
}
