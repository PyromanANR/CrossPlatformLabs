using LAB5.Services;
using Microsoft.AspNetCore.Mvc;

namespace LAB5.Controllers
{
    public class AddressController : Controller
    {
        private readonly ApiService _apiService;

        public AddressController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            // Fetch data from both API versions
            var addressV1 = await _apiService.GetAddressAsync(3, "v1");
            var addressV2 = await _apiService.GetAddressAsync(3, "v2");

            // Pass both versions to the view via ViewData
            ViewData["AddressV1"] = addressV1;
            ViewData["AddressV2"] = addressV2;

            return View();
        }
    }
}
