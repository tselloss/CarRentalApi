using AutoMapper;
using Cars.Entities;
using Cars.Info.Interface;
using Cars.Info.Model;
using Cars.Info.Repository;
using Microsoft.AspNetCore.Mvc;
using User.Info.Interface;
using User.Info.Model;
using Users.Entities;

namespace CarRentalManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICars _cars;
        private readonly ILogger<CarsController> _logger;
        private readonly IMapper _mapper;
        private readonly CarsService _carsService;

        public CarsController(ICars cars, ILogger<CarsController> logger, IMapper mapper, CarsService carsService)
        {
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            _cars = cars ?? throw new ArgumentException(nameof(cars));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _carsService = carsService ?? throw new ArgumentNullException(nameof(carsService));
        }

        [HttpGet("api/cars")]
        public async Task<ActionResult<IEnumerable<CarsInfo>>> GetAllCarsAsync()
        {
            var cars = await _cars.GetAllCarsAsync();
            if (cars == null)
            {
                _logger.LogInformation("We have no cars on Db");
                return NoContent();
            }
            return Ok(_mapper.Map<IEnumerable<CarsInfo>>(cars));
        }

        [HttpGet("api/carsById/{id}")]
        public async Task<ActionResult<CarsInfo>> GetCarsInfoByIdAsync(int id)
        {
            var cars = await _cars.GetCarInfoByIdAsync(id);
            if (cars == null)
            {
                _logger.LogInformation($"We have no car on Db with this id: {id} ");
                return NoContent();
            }
            return Ok(_mapper.Map<CarsInfo>(cars));
        }

        [HttpPost("api/createUser")]
        public async Task<ActionResult<CarsInfo>> CreateUserAsync([FromBody] CarsInfo carsInfo)
        {
            var newCar = _mapper.Map<CarEntity>(carsInfo);
            await _cars.CreateNewCar(newCar);
            await _carsService.SaveChangesAsync();
            return Ok(newCar);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUser(int id, CarsInfo carsInfo)
        {
            var car = _mapper.Map<CarEntity>(carsInfo);
            var cars = await _cars.GetCarInfoByIdAsync(id);
            if (cars == null)
            {
                _logger.LogInformation($"We delete a user from Db with this id: {id} ");
                return NoContent();
            }
            _cars.DeleteUserAsync(id, car);

            return Ok(cars);
        }
    }
}
