using Cars.Entities;
using Microsoft.EntityFrameworkCore;

namespace PostgresData
{
    public class CarInfoContext : DbContext
    {
        public DbSet<CarEntity> CarsInfo { get; set; }

        public CarInfoContext(DbContextOptions<CarInfoContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<UserEntity>().HasData();
            base.OnModelCreating(modelBuilder);
        }
    }
}
