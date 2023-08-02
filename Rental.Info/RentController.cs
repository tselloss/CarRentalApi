using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RentInfo.Entities;
using RentInfo.Interface;
using RentInfo.Model;
using RentInfo.Repository;
using User.Info.Model;

namespace CarRentalManagment.Controllers
{
    [ApiController]
    [Route("api/rent")]
    public class RentController : ControllerBase
    {
        private readonly IRental _rental;
        public RentController(IRental rental)
        {
            _rental = rental ?? throw new ArgumentException(nameof(rental));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllReservationsAsync()
        {
            return await _rental.GetAllReservationsAsync(this);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserInfoByIdAsync(int id)
        {
            return await _rental.GetReservationInfoByIdAsync(this, id);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] RentalInfo request)
        {
            return await _rental.CreateReservation(this, request);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRental(int id)
        {
            return await _rental.DeleteReservationAsync(this, id);
        }
    }
}