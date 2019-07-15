using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    [Route("api/values")]
    [ApiController]
    
    public class ValuesController : ControllerBase
    {
        [Authorize]       
        [EnableCors("MyPolicy")]
        [HttpGet("getlogin")]
        public IActionResult GetLogin()
        {
            return Ok($"{User.Identity.Name}");
        }
    }
}