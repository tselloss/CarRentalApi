using CarRentalManagment.PostgresContext;
using Microsoft.EntityFrameworkCore;
using RentInfo.Entities;
using RentInfo.Interface;

namespace RentInfo.Repository
{
    public class RentalService : IRental
    {
        private readonly PostgresDbContext _context;
        public RentalService(PostgresDbContext postgresContext)
        {
            _context = postgresContext ?? throw new ArgumentException(nameof(postgresContext));
        }
        public async Task CreateReservation(RentalEntity rentalEntity)
        {
            _context.Add(rentalEntity);
        }

        public void DeleteReservationAsync(int id, RentalEntity rentalEntity)
        {
            _context.RentalInfo.Remove(rentalEntity);
        }

        public async Task<IEnumerable<RentalEntity>> GetAllReservationsAsync()
        {
            return await _context.RentalInfo.OrderBy(_ => _.Id).ToListAsync();
        }

        public async Task<RentalEntity?> GetReservationInfoByIdAsync(int id)
        {
            return await _context.RentalInfo.Where(_ => _.Id == id).FirstOrDefaultAsync();
        }

        public Task UpdateReservationAsync(RentalEntity rentalEntity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
