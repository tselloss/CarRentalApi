using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        //public int UserId { get; set; }
        //[ForeignKey("UserId")]
        //public UserEntity User { get; set; }
        //public int CarId { get; set; }
        //[ForeignKey("CarId")]
        //public CarEntity CarsInfo { get; set; }
    }
}
