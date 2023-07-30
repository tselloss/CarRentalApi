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
using Users.Entities;

namespace CarRentalApi.Services
{
    public class AuthService
    {
        public IConfiguration config { get; }
        public PostgresDbContext dbContext { get; }
        public ControllerBase controller { get; }

        public AuthService(ControllerBase controller, IConfiguration config, PostgresDbContext dbContext)
        {
            this.controller = controller;
            this.config = config;
            this.dbContext = dbContext;
        }
        public async Task<IActionResult> Login(UserAuthenticationDto request)
        {
            if (!dbContext.UserInfo.Any(_ => _.Username == request.Username))
            {
                return controller.BadRequest(new ErrorResponse() { message = ErrorMessages.USER_NOT_FOUND });
            }
            UserEntity user = await dbContext.UserInfo.Where(_ => _.Username == request.Username).FirstAsync();
            var verifyHashedPass = VerifyPassword(request.Password);
            if (user.Password != verifyHashedPass)
            {
                return controller.NotFound();
            }
            return controller.Ok(UserPresenter.GetUserPresenter(user, CreateToken(user)));
        }
        private string CreateToken(UserEntity user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("AppSettings:Token").Value));
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
