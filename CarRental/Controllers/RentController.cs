using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RentInfo.Entities;
using RentInfo.Interface;
using RentInfo.Model;
using RentInfo.Repository;
using User.Info.Interface;
using User.Info.Model;
using User.Info.Repository;
using Users.Entities;

namespace CarRentalManagment.Controllers
{
    public class RentController : ControllerBase
    {

        private readonly IRental _rental;
        private readonly ILogger<UserActionsController> _logger;
        private readonly IMapper _mapper;
        private readonly RentalService _rentalService;

        public RentController(IRental rental, ILogger<UserActionsController> logger, IMapper mapper, RentalService rentalService)
        {
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            _rental = rental ?? throw new ArgumentException(nameof(rental));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _rentalService = rentalService ?? throw new ArgumentNullException(nameof(rentalService));
        }

        [HttpGet("api/getAllRental")]
        public async Task<ActionResult<IEnumerable<RentalInfo>>> GetAllReservationsAsync()
        {
            var rental = await _rental.GetAllReservationsAsync();
            if (rental == null)
            {
                _logger.LogInformation("We have no reservations on Db");
                return NoContent();
            }
            return Ok(_mapper.Map<IEnumerable<RentalInfo>>(rental));
        }

        [HttpGet("api/rentById/{id}")]
        public async Task<ActionResult<RentalInfo>> GetUserInfoByIdAsync(int id)
        {
            var rentalId = await _rental.GetReservationInfoByIdAsync(id);
            if (rentalId == null)
            {
                _logger.LogInformation($"We have no reservation on Db with this id: {id} ");
                return NoContent();
            }
            return Ok(_mapper.Map<UserInfo>(rentalId));
        }

        [HttpPost("api/createRental")]
        public async Task<ActionResult<RentalInfo>> CreateUserAsync([FromBody] RentalInfo rentalInfo)
        {
            var newRent = _mapper.Map<RentalEntity>(rentalInfo);
            await _rental.CreateReservation(newRent);
            await _rentalService.SaveChangesAsync();
            return Ok(newRent);
        }
        //TODO check the function
        [HttpDelete]
        public async Task<ActionResult> DeleteUser(int id, RentalInfo rentalInfo)
        {
            var rental = _mapper.Map<RentalEntity>(rentalInfo);
            var rentalById = await _rental.GetReservationInfoByIdAsync(id);
            if (rentalById == null)
            {
                _logger.LogInformation($"We delete a reservation from Db with this id: {id} ");
                return NoContent();
            }
            _rental.DeleteReservationAsync(id, rental);

            return Ok(rental);
        }
    }
}