using CarRentalManagment.PostgresContext;
using Cars.Entities;
using Cars.Info.Interface;
using Microsoft.EntityFrameworkCore;

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

        public void DeleteCarAsync(CarEntity carEntity)
        {
            _context.CarsInfo.Remove(carEntity);
        }

        public async Task<IEnumerable<CarEntity>> GetAllCarsAsync()
        {
            return await _context.CarsInfo.OrderBy(_ => _.CarId).ToListAsync();
        }

        public async Task<CarEntity> GetCarInfoByIdAsync(int id)
        {
            return await _context.CarsInfo.Where(_ => _.CarId == id).FirstOrDefaultAsync();
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
