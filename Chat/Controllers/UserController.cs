using System;
using System.Threading.Tasks;
using Chat.UserManagement;
using Chat.ViewModels;
using DAL.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Chat.Controllers
{
    [Route("api/user")]
    [EnableCors("MyPolicy")]
    [Authorize]
    [ApiController]
    
    public class UserController : ControllerBase
    {
        private readonly UserManager _userManager = new UserManager();
        
        
        

        

    }
}