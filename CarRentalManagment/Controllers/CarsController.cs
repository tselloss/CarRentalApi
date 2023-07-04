using Cars.Interface;
using Cars.Model;
using Cars.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICars _cars;

        public CarsController()
        {
            _cars = new CarsService();
        }

        [HttpGet("api/cars")]
        public ActionResult<IEnumerable<CarsInfo>> GetAllCars()
        {
            return _cars.GetAllCars();
        }
    }
}
