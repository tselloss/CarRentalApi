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

        [HttpGet("all")]
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

        [HttpGet("{id}")]
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

        [HttpPost]
        public async Task<ActionResult<RentalInfo>> CreateUserAsync([FromBody] RentalInfo rentalInfo)
        {
            var newRent = _mapper.Map<RentalEntity>(rentalInfo);
            await _rental.CreateReservation(newRent);
            await _rentalService.SaveChangesAsync();
            return Ok(newRent);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRental(int id)
        {
            var rental = await _rental.GetReservationInfoByIdAsync(id);

            if (rental == null)
            {
                _logger.LogInformation($"We have no rent on Db with this id: {id} ");
                return NoContent();
            }
            _rental.DeleteReservationAsync(rental);
            await _rentalService.SaveChangesAsync();
            return Ok(rental);
        }
    }
}