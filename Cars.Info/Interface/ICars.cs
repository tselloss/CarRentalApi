using Cars.Entities;
using Cars.Info.Model;
using Users.Entities;

namespace Cars.Info.Interface
{
    public interface ICars
    {
        Task CreateNewCar(CarEntity carEntity);
        Task UpdateUserAsync(CarEntity carEntity);
        void DeleteUserAsync(int id, CarEntity carEntity);

        Task<IEnumerable<CarEntity>> GetAllCarsAsync();
        Task<CarEntity?> GetCarInfoByIdAsync(int id);
    }
}
