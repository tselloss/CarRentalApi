using Microsoft.AspNetCore.Mvc;
using Rental.Info.Requests;
using RentInfo.Entities;
using RentInfo.Model;

namespace RentInfo.Interface
{
    public interface IRental
    {
        Task<IActionResult> CreateReservation(ControllerBase controller, RentRequest request);

        Task UpdateReservationAsync(RentalEntity rentalEntity);
        Task<IActionResult> DeleteReservationAsync(ControllerBase controller, int id);

        Task<IActionResult> GetAllReservationsAsync(ControllerBase controller);
        Task<IActionResult> GetReservationInfoByIdAsync(ControllerBase contoller, int id);
    }
}
