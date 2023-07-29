﻿using CarRentalApi.Model;
using CarRentalApi.Presenters;
using CarRentalApi.Requests;
using CarRentalApi.Responses;
using CarRentalManagment.Extensions;
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
        public async Task<IActionResult> register(UserDto request)
        {
            if (dbContext.UserInfo.Any(u => u.Username == request.Username))
            {
                return controller.BadRequest(new ErrorResponse { message = ErrorMessages.USERNAME_EXISTS });
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            UserEntity user = new UserEntity();
            user.Username = request.Username;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Role = Roles.Client;
            dbContext.UserInfo.Add(user);
            dbContext.SaveChanges();

            return controller.Ok();
        }
        public async Task<IActionResult> login(UserDto request)
        {
            if(!dbContext.UserInfo.Any(_ => _.Username == request.Username))
            {
                return controller.BadRequest(new ErrorResponse() { message = ErrorMessages.USER_NOT_FOUND });
            }
            UserEntity user = await dbContext.UserInfo.Where(_ => _.Username == request.Username).FirstAsync();
            if (!VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return controller.BadRequest(new ErrorResponse() { message = ErrorMessages.WRONG_PASSWORD });
            }

            return controller.Ok(UserPresenter.GetUserPresenter(user, CreateToken(user)));
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        private string CreateToken(UserEntity user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("AppSettings:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }

    }
}
