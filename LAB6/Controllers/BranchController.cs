using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LAB6.Data;
using LAB6.Models;

namespace LAB6.Controllers
{
    [ApiController]
    [Route("api/branches")]
    public class BranchController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BranchController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/branches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Branch>>> GetBranches()
        {
            return await _context.Branches
                .Include(b => b.Address)
                .Include(b => b.Bank)
                .ToListAsync();
        }

        // GET: api/branches/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Branch>> GetBranch(Guid id)
        {
            var branch = await _context.Branches
                .Include(b => b.Address)
                .Include(b => b.Bank)
                .FirstOrDefaultAsync(b => b.BranchId == id);
            if (branch == null)
                return NotFound();

            return branch;
        }

        // POST: api/branches
        [HttpPost]
        public async Task<ActionResult<Branch>> CreateBranch(Branch branch)
        {
            if (!_context.Banks.Any(b => b.BankId == branch.BankId))
                return BadRequest("Invalid BankId");

            if (!_context.Addresses.Any(a => a.AddressId == branch.AddressId))
                return BadRequest("Invalid AddressId");

            _context.Branches.Add(branch);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBranch), new { id = branch.BranchId }, branch);
        }
    }
}
