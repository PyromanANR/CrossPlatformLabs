using LAB5.Services;
using Microsoft.AspNetCore.Mvc;

namespace LAB5.Controllers
{
    public class TimeController : Controller
    {
        private readonly ApiService _apiService;

        public TimeController(ApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string inputDate)
        {
            try
            {
                if (DateTime.TryParse(inputDate, out DateTime parsedDateTime))
                {
                    string ukrainianTime = await _apiService.ConvertTimeAsync(parsedDateTime);
                    ViewBag.UkrainianTime = ukrainianTime;
                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid date format. Please enter a valid date.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error: {ex.Message}";
            }

            return View();
        }
    }
}
