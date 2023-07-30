using CarRentalManagment.PostgresContext;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
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

        public async Task Register(UserEntity userEntity)
        {
            if (!_context.UserInfo.Any(u => u.Username == userEntity.Username))
            {
                var hashedPass = CreatePasswordHash(userEntity.Password);
                userEntity.Password = hashedPass;
                _context.UserInfo.Add(userEntity);
                _context.SaveChanges();
            }
        }

        private string CreatePasswordHash(string password)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(password);
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public async Task<IEnumerable<UserEntity>> GetAllUsersAsync()
        {
            return await _context.UserInfo.OrderBy(_ => _.UserId).ToListAsync();
        }

        public async Task<UserEntity?> GetUserInfoByIdAsync(int id)
        {
            return await _context.UserInfo.Where(_ => _.UserId == id).FirstOrDefaultAsync();
        }

        public Task UpdateUserAsync(UserEntity userEntity)
        {
            throw new NotImplementedException();
        }

        public void DeleteUserAsync(UserEntity userEntity)
        {
            _context.UserInfo.Remove(userEntity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
