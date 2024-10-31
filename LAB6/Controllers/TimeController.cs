using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LAB6.Controllers
{
    [ApiController]
    [Route("api/time")]
    [Authorize]
    public class TimeController : ControllerBase
    {
        [HttpGet("convert")]
        public IActionResult ConvertToUkraineTime([FromQuery] DateTime inputDate)
        {
            // Встановлюємо часовий пояс України (GMT+2 або GMT+3 в залежності від літнього часу)
            TimeZoneInfo ukraineTimeZone = TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time");

            // Конвертуємо введену дату в український час
            DateTime ukraineTime = TimeZoneInfo.ConvertTimeFromUtc(inputDate.ToUniversalTime(), ukraineTimeZone);

            // Форматуємо дату у відповідності до українського часу
            string formattedDate = ukraineTime.ToString("yyyy-MM-dd HH:mm:ss");

            return Ok(new { UkrainianTime = formattedDate });
        }
    }

}
