using LAB6.Data;
using LAB6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAB6.Controllers
{
    [ApiController]
    [Route("api/mobileaddresses")]
    public class MobileAddressController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MobileAddressController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()
        {
            return await _context.Addresses.ToListAsync();
        }

        // GET: api/addresses/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
                return NotFound();

            return address;
        }

        // POST: api/addresses
        [HttpPost]
        public async Task<ActionResult<Address>> CreateAddress(Address address)
        {
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAddress), new { id = address.AddressId }, address);
        }
    }
}
