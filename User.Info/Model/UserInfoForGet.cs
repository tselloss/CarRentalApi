using User.Info.Extensions;

namespace User.Info.Model
{
    public class UserInfoForGet
    {
        public string? Username { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string? City { get; set; }
        public int? PostalCode { get; set; }
        public Roles? Role { get; set; }
    }
}
