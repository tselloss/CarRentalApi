using CarRentalManagment.Extensions;

namespace CarRentalApi.Requests
{
    public class UserAuthLogin
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}


