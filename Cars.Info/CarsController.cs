using Cars.Info.Interface;
using Cars.Info.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalManagment.Controllers
{
    [Route("api/car")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICars _cars;

        public CarsController(ICars cars)
        {
            _cars = cars ?? throw new ArgumentException(nameof(cars));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCarsAsync()
        {
            return await _cars.GetAllCarsAsync(this);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarsInfoByIdAsync(int id)
        {
            return await _cars.GetCarInfoByIdAsync(this, id);
        }

        [HttpGet("{id}/image")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetImage(int id)
        {
            return await _cars.GetCarImage(this, id);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCarAsync([FromForm] CarsInfo request)
        {
            return await _cars.CreateNewCar(this, request);
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditCar(int id, [FromForm] CarsInfo request)
        {
            return await _cars.EditCar(this, id, request);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            return await _cars.DeleteCarAsync(this, id);
        }
    }
}
