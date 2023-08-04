using CarRentalManagment.Extensions;

namespace CarRentalApi.Requests
{
    public class UserAuthRegister
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }
    }
}


