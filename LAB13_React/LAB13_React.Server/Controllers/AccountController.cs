using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LAB13_React.Server.Models;

namespace LAB13_React.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        [HttpGet("login")]
        public async Task<IActionResult> Login([FromQuery] string returnUrl = "/")
        {
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                .WithRedirectUri(returnUrl)
                .Build();
            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            return Ok();
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
                .WithRedirectUri("/")
                .Build();
            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }

        [Authorize]
        [HttpGet("user")]
        public IActionResult GetUser()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Ok(new { IsAuthenticated = false });
            }

            var userProfile = new UserProfileViewModel
            {
                Username = User.Claims.FirstOrDefault(c => c.Type == "nickname")?.Value,
                FullName = User.Claims.FirstOrDefault(c => c.Type == "name")?.Value,
                EmailAddress = User.Claims.FirstOrDefault(c => c.Type == "email")?.Value,
                PhoneNumber = User.Claims.FirstOrDefault(c => c.Type == "user_metadata.Phone")?.Value,
                ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value
            };

            return Ok(new
            {
                IsAuthenticated = true,
                Profile = userProfile
            });
        }
    }
}