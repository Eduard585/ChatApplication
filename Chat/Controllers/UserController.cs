using System;
using System.Threading.Tasks;
using Chat.UserManagement;
using Chat.ViewModels;
using DAL.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager _userManager = new UserManager();
        [EnableCors("MyPolicy")]
        [HttpPost("registration")]
        public  IActionResult Registration([FromBody]RestUserRegistrationInfo userModel)
        {
            var user = CreateUser(userModel);
            var registrationResult = _userManager.CreateUser(user);
            return Ok(registrationResult);
        }

        private static User CreateUser(RestUserRegistrationInfo userModel)
        {
            var user = new User
            {
                Email = userModel.Email,
                Login = userModel.Login,
                Password = userModel.Password,
                IsBlocked = false,
                UpdDate = DateTime.Now
            };
            return user;
        }

    }
}