using Cars.Model;
using System.ComponentModel.DataAnnotations;
using Users.Model;

namespace RentInfo.Entities
{
    public class RentalEntities
    {
        [Required]
        public UserInfo User { get; set; }
        [Required]
        public CarsInfo Car { get; set; }
        [Required]
        public DateTime DateFrom { get; set; }
        [Required]
        public DateTime DateTo { get; set; }
    }
}
