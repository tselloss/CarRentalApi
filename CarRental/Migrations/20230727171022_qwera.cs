using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarRental.Migrations
{
    /// <inheritdoc />
    public partial class qwera : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UsersInfo",
                columns: new[] { "Id", "Address", "City", "Email", "Password", "PostalCode", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "123 Main Street", "New York", "john.doe@example.com", "p@ssw0rd", 10001, "User", "JohnDoe" },
                    { 2, "456 Elm Avenue", "Los Angeles", "jane.smith@example.com", "s3cur3p@ss", 90001, "User", "JaneSmith" },
                    { 3, "789 Oak Street", "Chicago", "admin@example.com", "adm!n123", 60601, "Admin", "AdminUser" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UsersInfo",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UsersInfo",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UsersInfo",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
