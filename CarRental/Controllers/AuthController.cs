using AutoMapper.Internal;
using CarRentalApi.Requests;
using CarRentalApi.Services;
using CarRentalManagment.PostgresContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private AuthService authService;
        public AuthController(IConfiguration configuration, PostgresDbContext dataContext)
        {
            authService = new AuthService(this, configuration, dataContext);
        }

        [HttpPost("register")]
        public async Task<IActionResult> register(UserDto request)
        {

            return await authService.register(request);
        }

        [HttpPost("login")]
        public async Task<IActionResult> login(UserDto request)
        {
            return await authService.login(request);
        }
        [Authorize]
        [HttpPost("test")]
        public async Task<IActionResult> test()
        {
            return Ok();
        }

    }
}
