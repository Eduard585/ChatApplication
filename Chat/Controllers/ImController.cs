using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    [Route("api/im")]
    [ApiController]
    [Authorize]
    public class ImController : ControllerBase
    {
        private UserManager _userManager = new UserManager();
        [HttpGet]
        public IActionResult GetInfo()
        {
            JsonResult asd = new JsonResult("asda");
            return Ok(asd);
        }
    }
}