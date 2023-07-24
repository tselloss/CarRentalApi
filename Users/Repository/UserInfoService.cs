using Microsoft.EntityFrameworkCore;
using PostgresData;
using Users.Entities;
using Users.Interface;

namespace Users.Repository
{
    public class UserInfoService : IUserInfo
    {
        private readonly UsersContext _context;
        public UserInfoService(UsersContext postgresContext)
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
            return await _context.users.OrderBy(_ => _.Id).ToListAsync();
        }

        public async Task<UserEntity> GetUserInfoByIdAsync(int id)
        {
            return await _context.users.Where(_ => _.Id == id).FirstOrDefaultAsync();
        }

        public Task UpdateUserAsync(UserEntity userEntity)
        {
            throw new NotImplementedException();
        }

        public void DeleteUserAsync(int id, UserEntity userEntity)
        {
            _context.users.Remove(userEntity);
            _context.SaveChanges();
        }
    }
}
