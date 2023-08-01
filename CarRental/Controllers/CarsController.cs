using AutoMapper;
using Cars.Entities;
using Cars.Info.Interface;
using Cars.Info.Model;
using Cars.Info.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalManagment.Controllers
{
    [Route("api/car")]
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

        [HttpGet("all")]
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

        [HttpGet("{id}")]
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

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CarsInfo>> CreateCarAsync([FromBody] CarsInfo carsInfo)
        {
            var newCar = _mapper.Map<CarEntity>(carsInfo);
            await _cars.CreateNewCar(newCar);
            await _carsService.SaveChangesAsync();
            return Ok(newCar);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteCar(int id)
        {
            var cars = await _cars.GetCarInfoByIdAsync(id);
            if (cars == null)
            {
                _logger.LogInformation($"We have no car on Db with this id: {id} ");
                return NoContent();
            }
            _cars.DeleteCarAsync(cars);
            await _carsService.SaveChangesAsync();
            return Ok(cars);
        }
    }
}
