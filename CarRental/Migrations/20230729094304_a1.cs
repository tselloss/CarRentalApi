using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarRental.Migrations
{
    /// <inheritdoc />
    public partial class a1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarsInfo",
                columns: table => new
                {
                    CarId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Brand = table.Column<string>(type: "text", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false),
                    Seats = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarsInfo", x => x.CarId);
                });

            migrationBuilder.CreateTable(
                name: "UsersInfo",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    Address = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    PostalCode = table.Column<int>(type: "integer", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersInfo", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "RentalInfo",
                columns: table => new
                {
                    RentalId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateFrom = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateTo = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    CarsCarId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalInfo", x => x.RentalId);
                    table.ForeignKey(
                        name: "FK_RentalInfo_CarsInfo_CarsCarId",
                        column: x => x.CarsCarId,
                        principalTable: "CarsInfo",
                        principalColumn: "CarId");
                    table.ForeignKey(
                        name: "FK_RentalInfo_UsersInfo_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersInfo",
                        principalColumn: "UserId");
                });

            migrationBuilder.InsertData(
                table: "CarsInfo",
                columns: new[] { "CarId", "Brand", "Image", "Model", "Price", "Seats" },
                values: new object[,]
                {
                    { 1, "Toyota", "toyota_camry.jpg", "Camry", 25000f, 5 },
                    { 2, "Honda", "honda_civic.jpg", "Civic", 22000f, 5 },
                    { 3, "Ford", "ford_mustang.jpg", "Mustang", 35000f, 4 }
                });

            migrationBuilder.InsertData(
                table: "RentalInfo",
                columns: new[] { "RentalId", "CarsCarId", "DateFrom", "DateTo", "UserId" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2023, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, null, new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.InsertData(
                table: "UsersInfo",
                columns: new[] { "UserId", "Address", "City", "Email", "Password", "PostalCode", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "123 Main Street", "New York", "john.doe@example.com", "p@ssw0rd", 10001, 0, "JohnDoe" },
                    { 2, "456 Elm Avenue", "Los Angeles", "jane.smith@example.com", "s3cur3p@ss", 90001, 0, "JaneSmith" },
                    { 3, "789 Oak Street", "Chicago", "admin@example.com", "adm!n123", 60601, 0, "AdminUser" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RentalInfo_CarsCarId",
                table: "RentalInfo",
                column: "CarsCarId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalInfo_UserId",
                table: "RentalInfo",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentalInfo");

            migrationBuilder.DropTable(
                name: "CarsInfo");

            migrationBuilder.DropTable(
                name: "UsersInfo");
        }
    }
}
