using System;
using System.Threading.Tasks;
using Chat.UserManagement;
using Chat.ViewModels;
using DAL.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    [Route("api/user")]
    [ApiController]
    
    public class UserController : ControllerBase
    {
        private readonly UserManager _userManager = new UserManager();
        [EnableCors("MyPolicy")]
        [HttpPost("registration")]
        public  IActionResult Registration([FromBody]RestUserRegistrationInfo userModel)
        {           
            var registrationResult = _userManager.CreateUser(userModel);
            return Ok(registrationResult);
        }

        

    }
}