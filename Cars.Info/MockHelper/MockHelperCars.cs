using Cars.Info.Model;

namespace Cars.Info.MockHelper
{
    public class MockHelperCars
    {
        public List<CarsInfo> GetCarsList()
        {
            List<CarsInfo> carsList = new List<CarsInfo>()
            {
             new CarsInfo
            {
                CarId = 1,
                Brand = "Toyota",
                Model = "Camry",
                Seats = 5,
                Price = 25000.00f,
                Image = "toyota_camry.jpg"
            },
            new CarsInfo
            {
                CarId = 2,
                Brand = "Honda",
                Model = "Civic",
                Seats = 5,
                Price = 22000.00f,
                Image = "honda_civic.jpg"
            },
            new CarsInfo
            {
                CarId = 3,
                Brand = "Ford",
                Model = "Mustang",
                Seats = 4,
                Price = 35000.00f,
                Image = "ford_mustang.jpg"
            }
            };

            return carsList;
        }
    }
}
