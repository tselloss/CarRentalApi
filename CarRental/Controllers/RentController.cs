using Microsoft.AspNetCore.Mvc;
using RentInfo.Interface;
using RentInfo.Model;
using RentInfo.Repository;

namespace CarRentalManagment.Controllers
{
    public class RentController : ControllerBase
    {

        private readonly IRental _rental;
        private readonly ILogger<UserActionsController> _logger;

        public RentController(ILogger<UserActionsController> logger)
        {
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            _rental = new RentalService();
        }

        [HttpGet("api/rent")]
        public ActionResult<IEnumerable<RentalInfo>> GetRents()
        {
            return Ok();
        }
    }
}
