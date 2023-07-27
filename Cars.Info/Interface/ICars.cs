using Cars.Info.Model;

namespace Cars.Info.Interface
{
    public interface ICars
    {
        public List<CarsInfo> GetAllCars();
        CarsInfo GetCarById(int id);
    }
}
