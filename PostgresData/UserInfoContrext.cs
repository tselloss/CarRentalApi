using Microsoft.EntityFrameworkCore;
using Users.Entities;

namespace PostgresData
{
    public class UserInfoContrext : DbContext
    {
        public DbSet<UserEntity> UsersInfo { get; set; }

        public UserInfoContrext(DbContextOptions<UserInfoContrext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<UserEntity>().HasData();
            base.OnModelCreating(modelBuilder);
        }
    }
}

