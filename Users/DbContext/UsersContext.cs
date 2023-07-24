using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.Entities;
using Users.Interface;
using Users.Repository;

namespace PostgresData
{
    public class UsersContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<UserEntity> users { get; set; }

        public UsersContext(DbContextOptions<UsersContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<UsersContext>(options =>
                options.UseNpgsql(_configuration["ConnectionStrings:PostgreSQL"], b => b.MigrationsAssembly("CarRentalManagment")));

            services.AddScoped<IUserInfo, UserInfoService>();
        }
    }
}
