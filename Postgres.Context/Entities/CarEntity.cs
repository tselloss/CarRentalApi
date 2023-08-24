using Postgres.Context.Entities;
using RentInfo.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cars.Entities
{
    public class CarEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CarId { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public int? Seats { get; set; }
        [Required]
        public double Price { get; set; }
        public string Color { get; set; }
        public string Status { get; set; }
        public virtual AdminEntity Admin { get; set; }
        public virtual List<RentalEntity> Rents { get; set; } = new List<RentalEntity>();
    }
}
