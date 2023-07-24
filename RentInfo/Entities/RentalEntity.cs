using Cars.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Users.Model;

namespace RentInfo.Entities
{
    public class RentalEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]

        //TODO RELATIONS ON USERS AND CARS ENTITY
        public virtual UserInfo User { get; set; }
        [Required]
        public virtual CarsInfo Car { get; set; }
        [Required]
        public DateTime DateFrom { get; set; }
        [Required]
        public DateTime DateTo { get; set; }
    }
}
