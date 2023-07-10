﻿using Microsoft.AspNetCore.Mvc;
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

        public UserActionsController()
        {
            _userInfo = new UserInfoService(_connectionString);
        }
        [HttpGet("api/users")]
        public ActionResult<IEnumerable<UserInfo>> GetAllUsers()
        {
            return _userInfo.GetAllUsers();
        }
    }
}
