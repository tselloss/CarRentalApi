using CarRentalManagment.PostgresContext;
using Microsoft.EntityFrameworkCore;
using User.Info.Interface;
using Users.Entities;

namespace User.Info.Repository
{
    public class UserInfoService : IUserInfo
    {
        private readonly PostgresDbContext _context;
        public UserInfoService(PostgresDbContext postgresContext)
        {
            _context = postgresContext ?? throw new ArgumentException(nameof(postgresContext));
        }
        public async Task CreateUser(UserEntity userEntity)
        {
            _context.Add(userEntity);
        }

        public async Task<IEnumerable<UserEntity>> GetAllUsersAsync()
        {
            return await _context.UsersInfo.OrderBy(_ => _.UserId).ToListAsync();
        }

        public async Task<UserEntity?> GetUserInfoByIdAsync(int id)
        {
            return await _context.UsersInfo.Where(_ => _.UserId == id).FirstOrDefaultAsync();
        }

        public Task UpdateUserAsync(UserEntity userEntity)
        {
            throw new NotImplementedException();
        }

        public void DeleteUserAsync(UserEntity userEntity)
        {
            _context.UsersInfo.Remove(userEntity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
