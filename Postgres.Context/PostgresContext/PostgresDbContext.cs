using Cars.Entities;
using Microsoft.EntityFrameworkCore;
using RentInfo.Entities;
using Users.Entities;

namespace CarRentalManagment.PostgresContext
{
    public class PostgresDbContext : DbContext
    {
        public DbSet<UserEntity> UsersInfo { get; set; }
        public DbSet<CarEntity> CarsInfo { get; set; }
        public DbSet<RentalEntity> RentalInfo { get; set; }

        public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarEntity>()
                .HasData(
                new CarEntity
                {
                    CarId = 1,
                    Brand = "Toyota",
                    Model = "Camry",
                    Seats = 5,
                    Price = 25000.00f,
                    Image = "toyota_camry.jpg"
                },
                new CarEntity
                {
                    CarId = 2,
                    Brand = "Honda",
                    Model = "Civic",
                    Seats = 5,
                    Price = 22000.00f,
                    Image = "honda_civic.jpg"
                },
                new CarEntity
                {
                    CarId = 3,
                    Brand = "Ford",
                    Model = "Mustang",
                    Seats = 4,
                    Price = 35000.00f,
                    Image = "ford_mustang.jpg"
                });
            modelBuilder.Entity<UserEntity>()
                .HasData(
               new UserEntity
               {
                   UserId = 1,
                   Username = "JohnDoe",
                   Email = "john.doe@example.com",
                   Password = "p@ssw0rd",
                   Address = "123 Main Street",
                   City = "New York",
                   PostalCode = 10001,
                   Role = "User"
               },
            new UserEntity
            {
                UserId = 2,
                Username = "JaneSmith",
                Email = "jane.smith@example.com",
                Password = "s3cur3p@ss",
                Address = "456 Elm Avenue",
                City = "Los Angeles",
                PostalCode = 90001,
                Role = "User"
            },
            new UserEntity
            {
                UserId = 3,
                Username = "AdminUser",
                Email = "admin@example.com",
                Password = "adm!n123",
                Address = "789 Oak Street",
                City = "Chicago",
                PostalCode = 60601,
                Role = "Admin"
            });
            modelBuilder.Entity<RentalEntity>()
       .HasData(
           new RentalEntity
           {
               RentalId = 1,
               DateFrom = new DateTime(2023, 7, 30),
               DateTo = new DateTime(2023, 8, 5)
           },
           new RentalEntity
           {
               RentalId = 2,
               DateFrom = new DateTime(2023, 8, 1),
               DateTo = new DateTime(2023, 8, 8)
           }
       );
            base.OnModelCreating(modelBuilder);
        }
    }
}
