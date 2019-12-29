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
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;

namespace Chat.Controllers
{
    [Route("api/access")]
    [EnableCors("MyPolicy")]//TODO how to use tokens and login
    [ApiController]
    public class AccessController : Controller
    {
        private UserManager _userManager = new UserManager();
        public IActionResult Index()
        {
            return View("Index", new Dictionary<string, object> {{"layout", "_AuthLayout"}});
        }

        [HttpPost("registration")]
        public IActionResult Registration([FromBody]RestUserRegistrationInfo userModel)
        {
            var registrationResult = _userManager.CreateUser(userModel);
            return Ok(registrationResult);
        }

        
        [HttpPost("login")]
        public IActionResult Login([FromBody] RestUserLogin userLogin)
        {
            var login = userLogin.Login;
            var password = userLogin.Password;
            var response = new HttpResponseMessage();

            var loginResult = _userManager.Login(login, password);
            if (!loginResult.Success)
            {
                Response.StatusCode = 400;
                return BadRequest(loginResult.Error);
            }
            var restToken = CreateRestToken(loginResult);

            return Ok(new RestToken
            {
                AccessToken = restToken.AccessToken,
                ExpiresIn = 1000000
            });
        }
      
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
                ExpiresIn = 1000000
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

        

        private ClaimsIdentity GetIdentity(LoginResult loginResult)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, loginResult.UserId.ToString())
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

    }
}