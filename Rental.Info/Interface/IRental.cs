using Microsoft.AspNetCore.Mvc;
using RentInfo.Entities;
using RentInfo.Model;

namespace RentInfo.Interface
{
    public interface IRental
    {
        Task<IActionResult> CreateReservation(ControllerBase controller, RentalInfo request);

        Task UpdateReservationAsync(RentalEntity rentalEntity);
        Task<IActionResult> DeleteReservationAsync(ControllerBase controller, int id);

        Task<IActionResult> GetAllReservationsAsync(ControllerBase controller);
        Task<IActionResult> GetReservationInfoByIdAsync(ControllerBase contoller, int id);
    }
}
