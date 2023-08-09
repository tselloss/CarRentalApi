using CarRentalApi.Model;
using Cars.Entities;

namespace Cars.Info.Responses
{
    public class CarPresenter
    {
        public int CarId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int? Seats { get; set; }
        public double Price { get; set; }
        public int AdminId { get; set; }
        public string Color { get; set; }
        public string Status { get; set; }
        public string Image { get; set; }
        public List<String> ExcludedMonths { get; set; }

        public List<int> Rents { get; set; } = new List<int>();

        public static CarPresenter GetPresenter(CarEntity car)
        {
            CarPresenter carPresenter = BuildPresenter(car);

            List<int> rents = new List<int>();
            List<String> excludedDates = new List<string>();
            foreach (var rent in car.Rents)
            {
                rents.Add(rent.RentalId);
                DateTime dateFrom = DateTimeOffset.FromUnixTimeSeconds(rent.DateFrom).DateTime;
                DateTime dateTo = DateTimeOffset.FromUnixTimeSeconds(rent.DateTo).DateTime;
                while (dateFrom < dateTo)
                {
                    excludedDates.Add(Tools.GetStringDate(dateFrom));
                    if (dateFrom.Month == 12)
                    {
                        dateFrom = dateFrom.AddYears(1);
                    }
                    dateFrom = dateFrom.AddMonths(1);
                }
            }
            carPresenter.ExcludedMonths = excludedDates;
            carPresenter.Rents = rents;
            return carPresenter;
        }
        public static List<CarPresenter> GetPresenter(List<CarEntity> cars)
        {
            List<CarPresenter> carsPresenter = new List<CarPresenter>();
            foreach (var car in cars)
            {
                carsPresenter.Add(BuildPresenter(car));
            }
            return carsPresenter;
        }
        public static CarPresenter BuildPresenter(CarEntity car)
        {
            String Image = null;
            if (car.Image != null)
            {
                Image = "https://localhost:7104/api/car/" + car.CarId + "/image";
            }
            return new CarPresenter()
            {
                CarId = car.CarId,
                Brand = car.Brand,
                Model = car.Model,
                Seats = car.Seats,
                Price = car.Price,
                Color = car.Color,
                Status = car.Status,
                Image = Image,
                AdminId = car.Admin.UserId
            };
        }
    }
}
