using AutoMapper;
using CarRentalApi.Model;
using CarRentalApi.Presenters;
using CarRentalApi.Requests;
using CarRentalApi.Responses;
using CarRentalManagment.Controllers;
using CarRentalManagment.PostgresContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;
using User.Info.Interface;
using User.Info.Model;
using User.Info.Request;
using User.Info.Response;
using Users.Entities;

namespace User.Info.Repository
{
    public class UserInfoService : IUserInfo
    {
        private readonly PostgresDbContext _context;
        private readonly IConfiguration _config;
        private readonly ILogger<UserActionsController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public UserInfoService(IConfiguration config, PostgresDbContext postgresContext, ILogger<UserActionsController> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = postgresContext ?? throw new ArgumentException(nameof(postgresContext));
            _config = config ?? throw new ArgumentException(nameof(config));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<IActionResult> Register(ControllerBase controller, UserAuthRegister request)
        {
            if (request.Email == null || request.Username == null || request.Password == null)
            {
                return controller.BadRequest(new ErrorResponse() { message = ErrorMessages.FILL_MANDATORY_FIELDS });
            }
            if (_context.UserInfo.Any(u => u.Username == request.Username))
            {
                return controller.BadRequest(new ErrorResponse() { message = ErrorMessages.USERNAME_EXISTS });
            }
            if (_context.UserInfo.Any(u => u.Email == request.Email))
            {
                return controller.BadRequest(new ErrorResponse() { message = ErrorMessages.EMAIL_EXISTS });
            }

            UserEntity userEntity = new UserEntity()
            {
                Email = request.Email,
                Username = request.Username,
                Password = request.Password,
                Role = request.Role
            };
            var hashedPass = CreatePasswordHash(userEntity.Password);
            userEntity.Password = hashedPass;
            _context.UserInfo.Add(userEntity);
            _context.SaveChanges();

            return controller.Ok();
        }

        private string CreatePasswordHash(string password)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(password);
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }
        public async Task<IActionResult> Login(ControllerBase controller , UserAuthLogin request)
        {
            if (!_context.UserInfo.Any(_ => _.Username == request.Username))
            {
                if (!_context.UserInfo.Any(_ => _.Email == request.Email))
                {
                    return controller.BadRequest(new ErrorResponse() { message = ErrorMessages.USER_NOT_FOUND });
                }
            }
            UserEntity user = _context.UserInfo.Where(_ => _.Username == request.Username || _.Email == request.Email ).First();
            var verifyHashedPass = VerifyPassword(request.Password);
            if (user.Password != verifyHashedPass)
            {
                return controller.NotFound();
            }
            return controller.Ok(UserPresenter.GetUserPresenter(user, Tools.CreateToken(_config, user), Tools.CreateValetKeyToken(_config, user.UserId.ToString(), Scope.Download)));
        }

        public async Task<ActionResult<IEnumerable<UserInfoForGet>>> GetAllUsersAsync(ControllerBase controller)
        {
            var users = await _context.UserInfo.OrderBy(_ => _.UserId).ToListAsync();
            if (users == null)
            {
                _logger.LogInformation("We have no users on Db");
                return controller.NoContent();
            }
            return controller.Ok(_mapper.Map<IEnumerable<UserInfoForGet>>(users));
        }

        public async Task<ActionResult<UserInfoForGet>> GetUserInfoByIdAsync(ControllerBase controller, int id)
        {
            var user = await _context.UserInfo.Where(_ => _.UserId == id).FirstOrDefaultAsync();
            if (user == null)
            {
                _logger.LogInformation($"We have no user on Db with this id: {id} ");
                return controller.BadRequest(new ErrorResponse() { message = ErrorMessages.USER_NOT_FOUND });
            }
            return controller.Ok(_mapper.Map<UserInfoForGet>(user));
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        private string VerifyPassword(string password)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(password);
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public async Task<IActionResult> DeleteUser(ControllerBase controller, int id)
        {
            var user = await _context.UserInfo.Where(_ => _.UserId == id).FirstOrDefaultAsync();
            if (user == null)
            {
                _logger.LogInformation($"We have no user on Db with this id: {id} ");
                return controller.BadRequest(new ErrorResponse() { message = ErrorMessages.USER_NOT_FOUND });
            }
            _context.UserInfo.Remove(user);
            await _context.SaveChangesAsync();
            return controller.Ok();

        }

        public async Task<IActionResult> EditUser(ControllerBase controller, UserEditRequest request)
        {
            UserEntity user = await Tools.GetUser(_httpContextAccessor, _context);
            if (user == null)
            {
                return controller.BadRequest(new ErrorResponse() { message = ErrorMessages.INVALID_TOKEN });
            }
            if (request.Address != null)
            {
                if (!request.Address.Equals(user.Address))
                {
                    user.Address = request.Address;
                }
            }
            if (request.City != null)
            {
                if (!request.City.Equals(user.City))
                {
                    user.City = request.City;
                }
            }

            if (request.PostalCode != 0)
            {
                if (!request.PostalCode.Equals(user.PostalCode))
                {
                    user.PostalCode = request.PostalCode;
                }
            }

            _context.SaveChanges();

            return controller.Ok(UserPresenter.GetUserPresenter(user));
        }

        public async Task<ActionResult<UserInfoForGet>> GetValetkey(ControllerBase controller)
        {
            UserEntity userEntity = await Tools.GetUser(_httpContextAccessor,_context);
            if (userEntity == null) { controller.BadRequest(new ErrorResponse() { message = ErrorMessages.INVALID_TOKEN }); }
            string valet_key = Tools.CreateValetKeyToken(_config, userEntity.UserId.ToString(), Scope.Download);
            return controller.Ok(new ValetKeyResponse() { ValetKey = valet_key });
        }
    }
}
