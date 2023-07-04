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
                startDate = DateTime.Now.AddMonths(-3),
                endDate = DateTime.Now.AddMonths(3)
            },
            new CarsInfo
            {
                Id = 2,
                Brand = "Honda",
                Model = "Civic",
                Seats = 5,
                isAvailable=true,
                isFavorite=false,
                startDate = DateTime.Now.AddMonths(-2),
                endDate = DateTime.Now.AddMonths(4)
            },
            new CarsInfo
            {
                Id = 3,
                Brand = "Ford",
                Model = "Mustang",
                Seats = 4,
                isAvailable=true,
                isFavorite=false,
                startDate = DateTime.Now.AddMonths(-1),
                endDate = DateTime.Now.AddMonths(5)
            }
            };

            return carsList;
        }
    }
}
