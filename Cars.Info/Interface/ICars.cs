using Cars.Entities;

namespace Cars.Info.Interface
{
    public interface ICars
    {
        Task CreateNewCar(CarEntity carEntity);
        Task UpdateUserAsync(CarEntity carEntity);
        void DeleteCarAsync(CarEntity carEntity);

        Task<IEnumerable<CarEntity>> GetAllCarsAsync();
        Task<CarEntity?> GetCarInfoByIdAsync(int id);
    }
}
