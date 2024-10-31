using Microsoft.AspNetCore.Mvc;
using LAB6.Data;
using LAB6.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAB6.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/addresses")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class AddressController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AddressController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Version 1 - Get by ID
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Address>> GetAddressByIdV1(int id)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null) return NotFound();
            return address;
        }


        // Version 2 - Get by ID, with additional info or relationships (for example)
        [HttpGet("{id}")]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<Address>> GetAddressByIdV2(int id)
        {
            var address = await _context.Addresses
                .Include(a => a.Branches)  // Optional: include related branches if needed
                .Include(a => a.Customers) // Optional: include related customers if needed
                .FirstOrDefaultAsync(a => a.AddressId == id);

            if (address == null) return NotFound();
            return address;
        }

    }
}
