using CarRentalApi.Requests;
using Microsoft.AspNetCore.Mvc;
using User.Info.Model;
using User.Info.Request;

namespace User.Info.Interface
{
    public interface IUserInfo
    {
        Task<ActionResult<IEnumerable<UserInfoForGet>>> GetAllUsersAsync(ControllerBase controller);
        Task<ActionResult<UserInfoForGet>> GetUserInfoByIdAsync(ControllerBase controller, int id);
        Task<IActionResult> Register(ControllerBase controller, UserAuthRegister request);
        Task<IActionResult> Login(ControllerBase controller, UserAuthLogin request);
        Task<IActionResult> DeleteUser(ControllerBase controller, int id);
        Task<IActionResult> EditUser(ControllerBase controller, UserEditRequest request);
    }
}
