using RentInfo.Entities;
using RentInfo.Model;
using Users.Entities;

namespace RentInfo.Interface
{
    public interface IRental
    {
        Task CreateReservation(RentalEntity rentalEntity);

        Task UpdateReservationAsync(RentalEntity rentalEntity);
        void DeleteReservationAsync(int id, RentalEntity rentalEntity);

        Task<IEnumerable<RentalEntity>> GetAllReservationsAsync();
        Task<RentalEntity?> GetReservationInfoByIdAsync(int id);
    }
}
