using System.ComponentModel.DataAnnotations;
using User.Info.Extensions;

namespace User.Info.Model
{
    public class UserInfo
    {
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int PostalCode { get; set; }
        public Roles Role { get; set; }
    }
}
