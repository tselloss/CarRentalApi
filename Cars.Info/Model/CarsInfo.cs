using Microsoft.AspNetCore.Http;

namespace Cars.Info.Model
{
    public class CarsInfo
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int? Seats { get; set; }
        public float? Price { get; set; }
        public string Color { get; set; }
        public string Status { get; set; }
        public IFormFile Image { get; set; }
    }
}
