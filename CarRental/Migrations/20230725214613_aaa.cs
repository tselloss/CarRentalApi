using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Migrations
{
    /// <inheritdoc />
    public partial class aaa : Migration
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

            migrationBuilder.CreateTable(
                name: "CarEntityRentalEntity",
                columns: table => new
                {
                    CarId = table.Column<int>(type: "integer", nullable: false),
                    RentalId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarEntityRentalEntity", x => new { x.CarId, x.RentalId });
                    table.ForeignKey(
                        name: "FK_CarEntityRentalEntity_CarsInfo_CarId",
                        column: x => x.CarId,
                        principalTable: "CarsInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarEntityRentalEntity_RentalInfo_RentalId",
                        column: x => x.RentalId,
                        principalTable: "RentalInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarEntityRentalEntity_RentalId",
                table: "CarEntityRentalEntity",
                column: "RentalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarEntityRentalEntity");

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
