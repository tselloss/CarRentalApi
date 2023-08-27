using CarRentalManagment.PostgresContext;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using User.Info.Model;
using Users.Entities;

namespace CarRentalApi.Model
{
    public class Tools
    {
        public static async Task<UserEntity> GetUser(IHttpContextAccessor context, PostgresDbContext dbContext)
        {
            var username = context.HttpContext?.User.FindFirst(ClaimTypes.Name).Value;
            if (username == null) { return null; }
            if (!dbContext.UserInfo.Any(u => u.Username == username))
            {
                return null;
            }
            return dbContext.UserInfo.Where(u => u.Username == username).First();
        }
        public static string GetStringDate(DateTime dateTime)
        {
            return DateTimeOffset.FromUnixTimeSeconds(ConvertToUnixTimestamp(dateTime)).ToString("yyyy-MM-ddTHH:mm:ss.fffzzz");
        }
        public static long ConvertToUnixTimestamp(DateTime dateTime)
        {
            DateTimeOffset dateTimeOffset = new DateTimeOffset(dateTime);
            TimeSpan timeSpan = dateTimeOffset.UtcDateTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)timeSpan.TotalSeconds;
        }
        public static string CreateToken(IConfiguration _config, UserEntity user)
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
        public static string CreateValetKeyToken(IConfiguration _config, string name, Scope scope)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, scope.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );

            var valetKey = new JwtSecurityTokenHandler().WriteToken(token);
            return valetKey;
        }

    }
}
