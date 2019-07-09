using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    [Route("/")]
    [ApiController]
    public class AccessController : Controller
    {
        public IActionResult Index()
        {
            return View("Index", new Dictionary<string, object> {{"layout", "_AuthLayout"}});
        }

    }
}