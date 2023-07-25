using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cars.Entities
{
    public class CarEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public int Seats { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public string Image { get; set; }
        public CarEntity(int id, string brand, string model, int seats, float price, string image)
        {
            Id = id;
            Brand = brand;
            Model = model;
            Seats = seats;
            Price = price;
            Image = image;
        }
    }
}
