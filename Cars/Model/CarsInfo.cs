namespace Cars.Model
{
    public class CarsInfo
    {
        public int Id { get; set; }
        public string Brand { get; set; }

        public string Model { get; set; }
        public int Seats { get; set; }
        public bool isAvailable { get; set; }
        public bool isFavourite { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

    }
}
