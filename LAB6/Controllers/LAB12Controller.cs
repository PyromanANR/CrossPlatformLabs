using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace LAB6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LAB12Controller : ControllerBase
    {
        private readonly ILogger<LAB12Controller> _logger;

        public LAB12Controller(ILogger<LAB12Controller> logger)
        {
            _logger = logger;
        }

        // POST: api/LAB12
        [HttpPost]
        public IActionResult Post([FromBody] JsonElement data)
        {
            if (data.ValueKind == JsonValueKind.Null)
            {
                return BadRequest("No data received.");
            }
            Console.WriteLine($"Received Data: {data}");

            _logger.LogInformation($"Received Data: {data}");

            return Ok(new { Message = "Data received successfully!", ReceivedData = data });
        }
    }
}
