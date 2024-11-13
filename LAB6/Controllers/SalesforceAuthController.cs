using LAB6.Models;
using LAB6.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Salesforce.Common;

namespace LAB6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesforceAuthController : ControllerBase
    {
        private readonly ISalesforceAuthService _salesforceAuthService;

        public SalesforceAuthController(ISalesforceAuthService salesforceAuthService)
        {
            _salesforceAuthService = salesforceAuthService;
        }

        // POST: api/salesforce/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserCredentials credentials)
        {
            if (credentials == null || string.IsNullOrEmpty(credentials.Username) || string.IsNullOrEmpty(credentials.Password))
            {
                return BadRequest("Invalid credentials.");
            }

            // Authenticate with Salesforce
            var authResult = await _salesforceAuthService.AuthenticateAsync(credentials.Username, credentials.Password, credentials.SecurityToken);

            if (authResult.IsSuccess)
            {
                return Ok(new { authResult.AuthToken, authResult.InstanceUrl });
            }

            return Unauthorized("Authentication failed.");
        }
    }

    public class UserCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string SecurityToken { get; set; }
    }
}
