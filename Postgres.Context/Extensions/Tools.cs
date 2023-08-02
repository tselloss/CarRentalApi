using CarRentalManagment.PostgresContext;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
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
    }
}
