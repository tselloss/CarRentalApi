using Cars.Entities;
using Microsoft.EntityFrameworkCore;
using RentInfo.Entities;
using Users.Entities;

namespace PostgresData
{
    public class PostgresContext : DbContext
    {
        public DbSet<UserEntity> UsersInfo { get; set; }
        public DbSet<CarEntity> CarsInfo { get; set; }
        public DbSet<RentalEntities> RentalInfo { get; set; }

        public PostgresContext(DbContextOptions<PostgresContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
