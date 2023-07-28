using RentInfo.Entities;

namespace RentInfo.Interface
{
    public interface IRental
    {
        Task CreateReservation(RentalEntity rentalEntity);

        Task UpdateReservationAsync(RentalEntity rentalEntity);
        void DeleteReservationAsync(RentalEntity rentalEntity);

        Task<IEnumerable<RentalEntity>> GetAllReservationsAsync();
        Task<RentalEntity?> GetReservationInfoByIdAsync(int id);
    }
}
