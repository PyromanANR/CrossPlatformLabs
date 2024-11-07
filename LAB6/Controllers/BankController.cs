using LAB6.Data;
using LAB6.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LAB6.Controllers
{
    [ApiController]
    [Route("api/banks")]
    public class BankController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BankController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/banks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bank>>> GetBanks()
        {
            return await _context.Banks.ToListAsync();
        }

        // GET: api/banks/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Bank>> GetBank(Guid id)
        {
            var bank = await _context.Banks.FindAsync(id);
            if (bank == null)
                return NotFound();

            return bank;
        }

        // POST: api/banks
        [HttpPost]
        public async Task<ActionResult<Bank>> CreateBank(Bank bank)
        {
            _context.Banks.Add(bank);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBank), new { id = bank.BankId }, bank);
        }
    }
}
