using Cars.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentInfo.Entities;
using Users.Entities;
using Users.Interface;
using Users.Repository;

namespace PostgresData
{
    public class PostgresContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<UserEntity> UsersInfo { get; set; }
        public DbSet<CarEntity> CarsInfo { get; set; }
        public DbSet<RentalEntity> RentalInfo { get; set; }

        public PostgresContext(DbContextOptions<PostgresContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
