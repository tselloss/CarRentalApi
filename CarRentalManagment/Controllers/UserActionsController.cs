using Microsoft.AspNetCore.Mvc;
using Users.Interface;
using Users.Model;
using Users.Repository;

namespace CarRentalManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserActionsController : ControllerBase
    {
        private readonly IUserInfo _userInfo;
        private string _connectionString;
        private readonly ILogger<UserActionsController> _logger;

        public UserActionsController(ILogger<UserActionsController> logger)
        {
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            _userInfo = new UserInfoService();
        }



        [HttpGet("api/users")]
        public ActionResult<IEnumerable<UserInfo>> GetAllUsers()
        {
            var users = _userInfo.GetAllUsers();
            if (users == null)
            {
                _logger.LogInformation("We have no users on Db");
                return NoContent();
            }
            return Ok(users);
        }

        [HttpGet("api/userById/{id}")]
        public ActionResult<UserInfo> GetUserInfoById(int id)
        {
            var user = _userInfo.GetUserInfoById(id);
            if (user == null)
            {
                _logger.LogInformation($"We have no user on Db with this id: {id} ");
                return NoContent();
            }
            return Ok(user);
        }
    }
}
