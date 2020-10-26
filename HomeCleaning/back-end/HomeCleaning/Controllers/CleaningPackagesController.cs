using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeCleaning.Domain;
using HomeCleaning.Persistance.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeCleaning.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CleaningPackagesController : ControllerBase
    {
        private readonly HomeCleaningContext _context;

        public CleaningPackagesController(HomeCleaningContext context)
        {
            _context = context;
        }

        // GET: api/CleaningPackages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CleaningPackage>>> GetCleaningPackage()
        {
            return await _context.CleaningPackage.ToListAsync();
        }

        // GET: api/CleaningPackages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CleaningPackage>> GetCleaningPackage(int id)
        {
            var cleaningPackage = await _context.CleaningPackage.FindAsync(id);

            if (cleaningPackage == null)
            {
                return NotFound();
            }

            return cleaningPackage;
        }

        [HttpGet("GetCleaningPackagesByCategoryId")]
        public async Task<ActionResult<IEnumerable<CleaningPackage>>> GetCleaningPackagesByCategoryId(int categoryId)
        {
            var CleaningPackages = await _context.CleaningPackage.Where(a => a.CleaningCategoryId == categoryId).ToListAsync();
            return CleaningPackages;
        }

        // PUT: api/CleaningPackages/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCleaningPackage(int id, CleaningPackage cleaningPackage)
        {
            if (id != cleaningPackage.Id)
            {
                return BadRequest();
            }

            _context.Entry(cleaningPackage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CleaningPackageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CleaningPackages
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CleaningPackage>> PostCleaningPackage(CleaningPackage cleaningPackage)
        {
            _context.CleaningPackage.Add(cleaningPackage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCleaningPackage", new { id = cleaningPackage.Id }, cleaningPackage);
        }

        // DELETE: api/CleaningPackages/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CleaningPackage>> DeleteCleaningPackage(int id)
        {
            var cleaningPackage = await _context.CleaningPackage.FindAsync(id);
            if (cleaningPackage == null)
            {
                return NotFound();
            }

            _context.CleaningPackage.Remove(cleaningPackage);
            await _context.SaveChangesAsync();

            return cleaningPackage;
        }

        private bool CleaningPackageExists(int id)
        {
            return _context.CleaningPackage.Any(e => e.Id == id);
        }
    }
}
