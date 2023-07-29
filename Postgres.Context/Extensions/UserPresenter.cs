using CarRentalApi.Model;
using Users.Entities;

namespace CarRentalApi.Presenters
{
    public class UserPresenter
    {
        public int Id { get; set; }
        public String Username { get; set; } = string.Empty;
        public String token { get; set; } = string.Empty;

        public static UserPresenter GetUserPresenter(UserEntity user, String token)
        {
            return new UserPresenter
            {
                Id = user.UserId,
                Username = user.Username,
                token = token
            };
        }
        public static UserPresenter GetUserPresenter(UserEntity user)
        {
            return new UserPresenter
            {
                Id = user.UserId,
                Username = user.Username
            };
        }

        public static List<UserPresenter> GetUsersPresenter(List<UserEntity> users)
        {
            List<UserPresenter> presenters = new List<UserPresenter>();
            foreach (var user in users)
            {
                presenters.Add(new UserPresenter
                {
                    Id = user.UserId,
                    Username = user.Username,
                });
            }
            return presenters;
        }

    }
}
