using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Users.Entities
{
    public class UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(16)]
        public string Password { get; set; }
        [Required]
        [MaxLength(50)]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [MaxLength(7)]
        public int PostalCode { get; set; }
        [Required]
        public string Role { get; set; }

        public UserEntity(string username, string email, string password, string address, string city, int postalCode, string role)
        {
            Username = username;
            Email = email;
            Password = password;
            Address = address;
            City = city;
            PostalCode = postalCode;
            Role = role;
        }
    }
}
