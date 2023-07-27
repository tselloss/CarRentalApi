using Cars.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Users.Entities;

namespace RentInfo.Entities
{
    public class RentalEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        public DateTime DateFrom { get; set; }
        [Required]
        public DateTime DateTo { get; set; }
        [Required]
        public int CarId { get; set; }
        [Required]
        public int UserId { get; set; }
        public CarEntity Car { get; set; }
        public UserEntity User { get; set; }
    }
}
