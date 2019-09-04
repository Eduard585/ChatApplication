using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.UserManagement;
using Chat.FriendsManagement;
using Chat.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    [Route("api/im")]   
    [EnableCors("MyPolicy")]  
    [ApiController]
    public class ImController : ControllerBase
    {
        private UserManager _userManager = new UserManager();
        private FriendsManager _friendsManager = new FriendsManager();
        private UserIdentity _userIdentity = new UserIdentity();

        [HttpGet]
        public IActionResult GetInfo()
        {
            
            JsonResult asd = new JsonResult("asda");
            return Ok(asd);
        }

        [Authorize]
        [HttpGet("friends")]
        public IActionResult GetMyFriends()
        {
            var userId = _userIdentity.GetUserId(User);
            var friends =_friendsManager.GetFriends(userId);
            return Ok(friends);
        }
    }
}