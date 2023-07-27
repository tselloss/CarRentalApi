using CarRentalManagment.PostgresContext;
using Microsoft.EntityFrameworkCore;
using Users.Entities;
using Users.Interface;

namespace Users.Repository
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
            _context.SaveChanges();
        }

        public async Task<IEnumerable<UserEntity>> GetAllUsersAsync()
        {
            return await _context.UsersInfo.OrderBy(_ => _.Id).ToListAsync();
        }

        public async Task<UserEntity> GetUserInfoByIdAsync(int id)
        {
            return await _context.UsersInfo.Where(_ => _.Id == id).FirstOrDefaultAsync();
        }

        public Task UpdateUserAsync(UserEntity userEntity)
        {
            throw new NotImplementedException();
        }

        public void DeleteUserAsync(int id, UserEntity userEntity)
        {
            _context.UsersInfo.Remove(userEntity);
            _context.SaveChanges();
        }
    }
}
