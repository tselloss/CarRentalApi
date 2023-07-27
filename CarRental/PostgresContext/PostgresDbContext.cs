using Cars.Entities;
using Microsoft.EntityFrameworkCore;
using RentInfo.Entities;
using Users.Entities;

namespace CarRentalManagment.PostgresContext
{
    public class PostgresDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<UserEntity> UsersInfo { get; set; }
        public DbSet<CarEntity> CarsInfo { get; set; }
        public DbSet<RentalEntity> RentalInfo { get; set; }

        public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
