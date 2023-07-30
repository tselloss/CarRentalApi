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

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserAuthenticationDto request)
        {
            return await authService.Login(request);
        }

        [Authorize]
        [HttpPost("test")]
        public async Task<IActionResult> test()
        {
            return Ok();
        }

    }
}
