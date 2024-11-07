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
    [Route("api/branchtypes")]
    public class RefBranchTypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RefBranchTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/branchtypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RefBranchType>>> GetBranchTypes()
        {
            return await _context.RefBranchTypes.ToListAsync();
        }
    }
}
