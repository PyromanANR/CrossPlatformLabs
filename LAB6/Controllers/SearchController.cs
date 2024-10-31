using LAB6.Data;
using LAB6.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LAB6.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SearchController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Пошук за транзакціями за часом
        [HttpGet("transactions")]
        public async Task<ActionResult<IEnumerable<Transaction>>> SearchTransactionsByDate(
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            if (!startDate.HasValue || !endDate.HasValue)
                return BadRequest("Потрібно вказати як початкову, так і кінцеву дату.");

            var transactions = await _context.Transactions
                .Where(t => t.TransactionDateTime >= startDate.Value && t.TransactionDateTime <= endDate.Value)
                .ToListAsync();

            return Ok(transactions);
        }

        // Пошук за клієнтами по PersonalDetails
        [HttpGet("customers")]
        public async Task<ActionResult<IEnumerable<Customer>>> SearchCustomersByPersonalDetails(string personalDetail)
        {
            if (string.IsNullOrEmpty(personalDetail))
                return BadRequest("Необхідно вказати значення для пошуку.");

            var customers = await _context.Customers
                .Where(c => c.PersonalDetails.Contains(personalDetail))
                .ToListAsync();

            return Ok(customers);
        }

        // Пошук акаунтів за списком ID, з JOIN до транзакцій та типів транзакцій
        [HttpGet("accounts")]
        public async Task<ActionResult<IEnumerable<Account>>> SearchAccountsByIdsWithTransactionsAndTypes(
            [FromQuery] List<int> accountIds)
        {
            if (accountIds == null || accountIds.Count == 0)
                return BadRequest("Необхідно надати список ID акаунтів.");

            var accounts = await _context.Accounts
                .Where(a => accountIds.Contains(a.AccountNumber))
                .Include(a => a.Transactions)
                    .ThenInclude(t => t.TransactionType)
                .ToListAsync();

            return Ok(accounts);
        }
    }
}
