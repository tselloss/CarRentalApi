using AutoMapper;
using CarRentalApi.Model;
using CarRentalApi.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Info.Interface;
using User.Info.Model;
using User.Info.Repository;
using Users.Entities;

namespace CarRentalManagment.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserActionsController : ControllerBase
    {
        private readonly IUserInfo _userInfo;
        private readonly ILogger<UserActionsController> _logger;
        private readonly IMapper _mapper;
        private readonly UserInfoService _userInfoService;

        public UserActionsController(IUserInfo userInfo, ILogger<UserActionsController> logger, IMapper mapper, UserInfoService userInfoService)
        {
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            _userInfo = userInfo ?? throw new ArgumentException(nameof(userInfo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userInfoService = userInfoService ?? throw new ArgumentNullException(nameof(userInfoService));
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UserInfoForGet>>> GetAllUsersAsync()
        {
            var users = await _userInfo.GetAllUsersAsync();
            if (users == null)
            {
                _logger.LogInformation(ErrorMessages.ITEM_NOT_FOUND);
                return NoContent();
            }
            return Ok(_mapper.Map<IEnumerable<UserInfoForGet>>(users));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserInfoForGet>> GetUserInfoByIdAsync(int id)
        {
            var user = await _userInfo.GetUserInfoByIdAsync(id);
            if (user == null)
            {
                _logger.LogInformation(ErrorMessages.ITEM_NOT_FOUND + $" User to find by id: {id} ");
                return NoContent();
            }
            return Ok(_mapper.Map<UserInfoForGet>(user));
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var users = await _userInfo.GetUserInfoByIdAsync(id);

            if (users == null)
            {
                _logger.LogInformation(ErrorMessages.ITEM_NOT_FOUND + $" User to delete with id: {id} ");
                return NoContent();
            }
            _userInfo.DeleteUserAsync(users);
            await _userInfoService.SaveChangesAsync();
            return Ok(users);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserInfo userInfo)
        {
            var newUser = _mapper.Map<UserEntity>(userInfo);
            await _userInfo.Register(newUser);
            await _userInfoService.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserAuthenticationDto request)
        {
            return await _userInfo.Login(this, request);
        }
    }
}
