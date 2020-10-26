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
    public class CleaningCategoriesController : ControllerBase
    {
        private readonly HomeCleaningContext _context;

        public CleaningCategoriesController(HomeCleaningContext context)
        {
            _context = context;
        }

        // GET: api/CleaningCategories
        [HttpGet]
        //  [Authorize]
        public async Task<ActionResult<IEnumerable<CleaningCategory>>> GetCleaningCategory()
        {
            return await _context.CleaningCategory.ToListAsync();
        }

        // GET: api/CleaningCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CleaningCategory>> GetCleaningCategory(int id)
        {
            var cleaningCategory = await _context.CleaningCategory.FindAsync(id);

            if (cleaningCategory == null)
            {
                return NotFound();
            }

            return cleaningCategory;
        }

        // PUT: api/CleaningCategories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCleaningCategory(int id, CleaningCategory cleaningCategory)
        {
            if (id != cleaningCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(cleaningCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CleaningCategoryExists(id))
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

        // POST: api/CleaningCategories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CleaningCategory>> PostCleaningCategory(CleaningCategory cleaningCategory)
        {
            _context.CleaningCategory.Add(cleaningCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCleaningCategory", new { id = cleaningCategory.Id }, cleaningCategory);
        }

        // DELETE: api/CleaningCategories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CleaningCategory>> DeleteCleaningCategory(int id)
        {
            var cleaningCategory = await _context.CleaningCategory.FindAsync(id);
            if (cleaningCategory == null)
            {
                return NotFound();
            }

            _context.CleaningCategory.Remove(cleaningCategory);
            await _context.SaveChangesAsync();

            return cleaningCategory;
        }

        private bool CleaningCategoryExists(int id)
        {
            return _context.CleaningCategory.Any(e => e.Id == id);
        }
    }
}
