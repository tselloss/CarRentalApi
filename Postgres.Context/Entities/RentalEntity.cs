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
        public int RentalId { get; set; }
        [Required]
        public DateTime DateFrom { get; set; }
        [Required]
        public DateTime DateTo { get; set; }
        public UserEntity User { get; set; }
        public CarEntity Cars { get; set; }
    }
}
