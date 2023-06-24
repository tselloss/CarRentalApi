using Microsoft.AspNetCore.Mvc;
using Users;

namespace CarRentalManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserActionsController : Controller
    {
        private readonly IUserInfo _userInfo;

        public UserActionsController(IUserInfo userInfo)
        {
            _userInfo = userInfo;
        }
        [HttpGet("api/users")]
        public IActionResult GetUsers()
        {
            var userFromRepo = _userInfo.GetUserInfoAsync();
            return new JsonResult(userFromRepo);
        }
    }
}
