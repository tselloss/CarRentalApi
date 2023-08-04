using AutoMapper;
using CarRentalApi.Model;
using Cars.Entities;
using Cars.Info.Interface;
using Cars.Info.Model;
using Cars.Info.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCarAsync([FromBody] CarsInfo request)
        {
            return await _cars.CreateNewCar(this, request);
        }
        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditCar(int id, [FromBody] CarsInfo request)
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
