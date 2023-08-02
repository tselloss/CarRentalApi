using CarRentalApi.Requests;
using CarRentalManagment.Controllers;
using Microsoft.AspNetCore.Mvc;
using User.Info.Model;
using Users.Entities;

namespace User.Info.Interface
{
    public interface IUserInfo
    {
        Task UpdateUserAsync(UserEntity userEntity);
        Task<ActionResult<IEnumerable<UserInfoForGet>>> GetAllUsersAsync(ControllerBase controller);
        Task<ActionResult<UserInfoForGet>> GetUserInfoByIdAsync(ControllerBase controller, int id);
        Task<IActionResult> Register(ControllerBase controller, UserInfo request);
        Task<IActionResult> Login(ControllerBase controller, UserAuthenticationDto request);
        Task<IActionResult> DeleteUser(ControllerBase controller, int id);
    }
}
