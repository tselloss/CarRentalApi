using CarRentalManagment.PostgresContext;
using Cars.Entities;
using Cars.Info.Interface;
using Microsoft.EntityFrameworkCore;
using Users.Entities;

namespace Cars.Info.Repository
{
    public class CarsService : ICars
    {
        private readonly PostgresDbContext _context;

        public CarsService(PostgresDbContext postgresContext)
        {
            _context = postgresContext ?? throw new ArgumentException(nameof(postgresContext));

        }
        public async Task CreateNewCar(CarEntity carEntity)
        {
            _context.Add(carEntity);
        }

        public void DeleteUserAsync(int id, CarEntity carEntity)
        {
            _context.CarsInfo.Remove(carEntity);
        }

        public async Task<IEnumerable<CarEntity>> GetAllCarsAsync()
        {
            return await _context.CarsInfo.OrderBy(_ => _.Id).ToListAsync();
        }

        public async Task<CarEntity> GetCarInfoByIdAsync(int id)
        {
            return await _context.CarsInfo.Where(_ => _.Id == id).FirstOrDefaultAsync();
        }

        public Task UpdateUserAsync(CarEntity carEntity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
