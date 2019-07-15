using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Chat.Auth;
using Chat.UserManagement;
using Chat.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Chat.Controllers
{
    [Route("api/access")]
    [ApiController]
    public class AccessController : Controller
    {
        private UserManager _userManager = new UserManager();
        public IActionResult Index()
        {
            return View("Index", new Dictionary<string, object> {{"layout", "_AuthLayout"}});
        }

        [EnableCors("MyPolicy")]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] RestUserLogin userLogin)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }

            var result = _userManager.Login(userLogin.Login, userLogin.Password);
            if (!result.Success)
            {
                return BadRequest(Json(result.Error));
            }

            var user = new IdentityUser {UserName = userLogin.Login};

            await Authenticate(result);
        
            return Content(User.Identity.Name);

        }

        [EnableCors("MyPolicy")]
        [HttpPost("token")]
        public IActionResult Token([FromBody] RestUserLogin userLogin)
        {
            var login = userLogin.Login;
            var password = userLogin.Password;

            var loginResult = _userManager.Login(login, password);
            if (!loginResult.Success)
            {
                Response.StatusCode = 400;               
                return BadRequest(loginResult.Error);
            }
            var response = CreateRestToken(loginResult);            
            Response.ContentType = "application/json";
            return Ok(new RestToken
            {
                AccessToken = response.AccessToken,
                ExpiresIn = 1000
            });
        }

        private RestToken CreateRestToken(LoginResult loginResult)
        {
            var identity = GetIdentity(loginResult);
            var now = DateTime.Now;
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new RestToken
            {
                AccessToken = encodedJwt,
                ExpiresIn = 10000
            };
            return response;
        }

        private async Task Authenticate(LoginResult loginResult)
        {
            var id = GetIdentity(loginResult);
            await HttpContext.SignInAsync("ChatAppCookie", new ClaimsPrincipal(id));
        }

        private ClaimsIdentity GetIdentity(LoginResult loginResult)//TODO add fields to LoginResult and to JWT
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, loginResult.UserName)
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

    }
}