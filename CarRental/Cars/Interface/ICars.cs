using Cars.Model;

namespace Cars.Interface
{
    public interface ICars
    {
        public List<CarsInfo> GetAllCars();
        CarsInfo GetCarById(int id);
    }
}
