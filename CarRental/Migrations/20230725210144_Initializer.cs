using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CarRental.Migrations
{
    /// <inheritdoc />
    public partial class Initializer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalInfo_UserInfo_UserId",
                table: "RentalInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalInfo_UsersInfo_UserEntityId",
                table: "RentalInfo");

            migrationBuilder.DropTable(
                name: "UserInfo");

            migrationBuilder.DropIndex(
                name: "IX_RentalInfo_UserEntityId",
                table: "RentalInfo");

            migrationBuilder.DropColumn(
                name: "UserEntityId",
                table: "RentalInfo");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalInfo_UsersInfo_UserId",
                table: "RentalInfo",
                column: "UserId",
                principalTable: "UsersInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalInfo_UsersInfo_UserId",
                table: "RentalInfo");

            migrationBuilder.AddColumn<int>(
                name: "UserEntityId",
                table: "RentalInfo",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Address = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    PostalCode = table.Column<int>(type: "integer", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RentalInfo_UserEntityId",
                table: "RentalInfo",
                column: "UserEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalInfo_UserInfo_UserId",
                table: "RentalInfo",
                column: "UserId",
                principalTable: "UserInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalInfo_UsersInfo_UserEntityId",
                table: "RentalInfo",
                column: "UserEntityId",
                principalTable: "UsersInfo",
                principalColumn: "Id");
        }
    }
}
