using CarRentalApi.Model;
using CarRentalManagment.Extensions;
using Users.Entities;

namespace CarRentalApi.Presenters
{
    public class UserPresenter
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Token { get; set; }
        public string ValetKey { get; set; }
        public string City { get; set; }
        public int PostalCode { get; set; }
        public Roles Role { get; set; }

        public static UserPresenter GetUserPresenter(UserEntity user, String token, String ValetKey)
        {
            return build_presenter(user, token, ValetKey);
        }
        public static UserPresenter GetUserPresenter(UserEntity user)
        {
            return build_presenter(user, null, null);
        }

        public static IEnumerable<UserPresenter> GetUsersPresenter(List<UserEntity> users)
        {
            List<UserPresenter> presenters = new List<UserPresenter>();
            foreach (UserEntity user in users)
            {
                presenters.Add(build_presenter(user, null, null));
            }
            return presenters;
        }
        private static UserPresenter build_presenter(UserEntity user, String Token, String ValetKey)
        {
            return new UserPresenter
            {
                Id = user.UserId,
                Username = user.Username,
                Token = Token,
                ValetKey = ValetKey,
                Address = user.Address,
                City = user.City,
                Email = user.Email,
                PostalCode = user.PostalCode,
                Role = user.Role
            };
        }

    }
}
