using LAB6.Data;
using LAB6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace LAB6.Controllers
{

    // Customer and Transaction Controllers for Reference Tables
    [ApiController]
    [Route("api/references")]
    public class ReferencesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReferencesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/references/customers
        [HttpGet("customers")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        // GET: api/references/customers/{id}
        [HttpGet("customers/{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound();

            return customer;
        }

        // GET: api/references/transactions
        [HttpGet("transactions")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            return await _context.Transactions.ToListAsync();
        }

        // GET: api/references/transactions/{id}
        [HttpGet("transactions/{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(Guid id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
                return NotFound();

            return transaction;
        }
    }

}
