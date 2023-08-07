using CarRentalApi.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Info.Interface;
using User.Info.Model;
using User.Info.Request;

namespace CarRentalManagment.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserActionsController : ControllerBase
    {
        private readonly IUserInfo _userInfo;

        public UserActionsController(IUserInfo userInfo)
        {
            _userInfo = userInfo ?? throw new ArgumentException(nameof(userInfo));
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserInfoForGet>>> GetAllUsersAsync()
        {
            return await _userInfo.GetAllUsersAsync(this);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserInfoForGet>> GetUserInfoByIdAsync(int id)
        {
            return await _userInfo.GetUserInfoByIdAsync(this, id);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserAuthRegister request)
        {
            return await _userInfo.Register(this, request);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserAuthLogin request)
        {
            return await _userInfo.Login(this, request);
        }

        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> EditUser([FromBody] UserEditRequest request)
        {
            return await _userInfo.EditUser(this, request);
        }
    }
}
