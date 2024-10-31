using LAB5.Services;
using Microsoft.AspNetCore.Mvc;
using LAB5.Models;

namespace LAB5.Controllers
{
    public class TableViewController : Controller
    {
        private readonly ApiService _apiService;

        public TableViewController(ApiService apiService)
        {
            _apiService = apiService;
        }

        // GET: TableView
        public async Task<IActionResult> Index()
        {
            var accounts = await _apiService.GetAccountsAsync();
            var customers = await _apiService.GetCustomersAsync();
            var transactions = await _apiService.GetTransactionsAsync(); // Fetch transactions

            var model = new AccountsAndCustomersAndTransactionViewModel
            {
                Accounts = accounts,
                Customers = customers,
                Transactions = transactions // Add transactions to the model
            };

            return View(model);
        }



        // GET: TableView/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var account = await _apiService.GetAccountByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }
    }
}
