using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Users.Entities;
using Users.Interface;
using Users.Model;

namespace CarRentalManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserActionsController : ControllerBase
    {
        private readonly IUserInfo _userInfo;
        private readonly ILogger<UserActionsController> _logger;
        private readonly IMapper _mapper;

        public UserActionsController(IUserInfo userInfo, ILogger<UserActionsController> logger, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            _userInfo = userInfo ?? throw new ArgumentException(nameof(userInfo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("api/users")]
        public async Task<ActionResult<IEnumerable<UserInfo>>> GetAllUsersAsync()
        {
            var users = await _userInfo.GetAllUsersAsync();
            if (users == null)
            {
                _logger.LogInformation("We have no users on Db");
                return NoContent();
            }
            return Ok(_mapper.Map<IEnumerable<UserInfo>>(users));
        }

        [HttpGet("api/userById/{id}")]
        public async Task<ActionResult<UserInfo>> GetUserInfoByIdAsync(int id)
        {
            var user = await _userInfo.GetUserInfoByIdAsync(id);
            if (user == null)
            {
                _logger.LogInformation($"We have no user on Db with this id: {id} ");
                return NoContent();
            }
            return Ok(_mapper.Map<IEnumerable<UserInfo>>(user));
        }

        [HttpPost("api/createUser")]
        public async Task<ActionResult<UserEntity>> CreateUserAsync(UserEntity userInfo)
        {
            var newUser = _mapper.Map<UserEntity>(userInfo);
            await _userInfo.CreateUser(newUser);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUser(int id, UserEntity userEntity)
        {
            var users = await _userInfo.GetUserInfoByIdAsync(id);
            if (users == null)
            {
                _logger.LogInformation($"We have no user on Db with this id: {id} ");
                return NoContent();
            }
            _userInfo.DeleteUserAsync(id, userEntity);

            return NoContent();
        }
    }
}
