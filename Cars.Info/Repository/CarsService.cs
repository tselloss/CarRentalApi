using Cars.Info.Interface;
using Cars.Info.MockHelper;
using Cars.Info.Model;

namespace Cars.Info.Repository
{
    public class CarsService : ICars
    {
        private MockHelperCars listOfCars = new MockHelperCars();
        public List<CarsInfo> GetAllCars()
        {
            try
            {
                return listOfCars.GetCarsList();
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        public CarsInfo GetCarById(int id)
        {
            return listOfCars.GetCarsList().FirstOrDefault(_ => _.Id == id);
        }
    }
}
