using CarRentalApi.Model;
using CarRentalApi.Presenters;
using CarRentalApi.Requests;
using CarRentalApi.Responses;
using CarRentalManagment.PostgresContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using User.Info.Interface;
using Users.Entities;

namespace User.Info.Repository
{
    public class UserInfoService : IUserInfo
    {
        private readonly PostgresDbContext _context;
        private readonly IConfiguration _config;
        public UserInfoService(IConfiguration config, PostgresDbContext postgresContext)
        {
            _context = postgresContext ?? throw new ArgumentException(nameof(postgresContext));
            _config = config ?? throw new ArgumentException(nameof(config));
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
        public async Task<IActionResult> Login(ControllerBase controller , UserAuthenticationDto request)
        {
            if (!_context.UserInfo.Any(_ => _.Username == request.Username))
            {
                return controller.BadRequest(new ErrorResponse() { message = ErrorMessages.USER_NOT_FOUND });
            }
            UserEntity user = _context.UserInfo.Where(_ => _.Username == request.Username).First();
            var verifyHashedPass = VerifyPassword(request.Password);
            if (user.Password != verifyHashedPass)
            {
                return controller.NotFound();
            }
            return controller.Ok(UserPresenter.GetUserPresenter(user, CreateToken(user)));
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
        private string CreateToken(UserEntity user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        private string VerifyPassword(string password)
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

    }
}
