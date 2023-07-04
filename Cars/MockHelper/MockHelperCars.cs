using Cars.Model;
namespace Cars.MockHelper
{
    public class MockHelperCars
    {
        public List<CarsInfo> GetCarsList()
        {
            List<CarsInfo> carsList = new List<CarsInfo>()
            {
            new CarsInfo
            {
                Id = 1,
                Brand = "Toyota",
                Model = "Corolla",
                Seats = 5,
                isAvailable=true,
                isFavorite=false,
                startDate = DateTime.Now.AddMonths(-3).Date,
                endDate = DateTime.Now.AddMonths(3).Date
            },
            new CarsInfo
            {
                Id = 2,
                Brand = "Honda",
                Model = "Civic",
                Seats = 5,
                isAvailable=true,
                isFavorite=false,
                startDate = DateTime.Now.AddMonths(-2).Date,
                endDate = DateTime.Now.AddMonths(4).Date
            },
            new CarsInfo
            {
                Id = 3,
                Brand = "Ford",
                Model = "Mustang",
                Seats = 4,
                isAvailable=true,
                isFavorite=false,
                startDate = DateTime.Now.AddMonths(-1).Date,
                endDate = DateTime.Now.AddMonths(5).Date
            }
            };

            return carsList;
        }
    }
}
