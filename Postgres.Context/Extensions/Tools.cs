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
    }
}
