using LAB5.Models;
using LAB5.Services;
using Microsoft.AspNetCore.Mvc;

namespace LAB5.Controllers
{
    public class SearchMvcController : Controller
    {
        private readonly ApiService _apiService;

        public SearchMvcController(ApiService apiService)
        {
            _apiService = apiService;
        }

        // GET: Search
        public IActionResult Index()
        {
            return View(new SearchViewModel());
        }

        // POST: Search/Transactions
        [HttpPost]
        public async Task<IActionResult> SearchTransactions(SearchViewModel model)
        {
            if (model.StartDate == null || model.EndDate == null)
            {
                ModelState.AddModelError("", "Please provide both start and end dates.");
                return View("Index", model);
            }

            try
            {
                model.Transactions = await _apiService.SearchTransactionsAsync(model.StartDate.Value, model.EndDate.Value);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View("Index", model);
        }

        // POST: Search/Customers
        [HttpPost]
        public async Task<IActionResult> SearchCustomers(SearchViewModel model)
        {
            if (string.IsNullOrEmpty(model.PersonalDetail))
            {
                ModelState.AddModelError("", "Please provide a value for searching customers.");
                return View("Index", model);
            }

            try
            {
                model.Customers = await _apiService.SearchCustomersAsync(model.PersonalDetail);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View("Index", model);
        }

        // POST: Search/Accounts
        [HttpPost]
        public async Task<IActionResult> SearchAccounts(SearchViewModel model)
        {
            if (string.IsNullOrEmpty(model.AccountIds))
            {
                ModelState.AddModelError("", "Please provide at least one account ID.");
                return View("Index", model);
            }

            // Split the input string into a list of integers
            var accountIds = model.AccountIds
                .Split(',')
                .Select(id => int.TryParse(id.Trim(), out var result) ? result : (int?)null)
                .Where(id => id.HasValue)
                .Select(id => id.Value)
                .ToList();

            if (!accountIds.Any())
            {
                ModelState.AddModelError("", "Please provide valid account IDs.");
                return View("Index", model);
            }

            // Fetch accounts using the account IDs
            try
            {
                model.Accounts = await _apiService.SearchAccountsAsync(accountIds);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error fetching accounts: {ex.Message}");
            }

            return View("Index", model);
        }

    }
}
