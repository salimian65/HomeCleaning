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
    public class CleaningExtraOptionsController : ControllerBase
    {
        private readonly HomeCleaningContext _context;

        public CleaningExtraOptionsController(HomeCleaningContext context)
        {
            _context = context;
        }

        // GET: api/CleaningExtraOptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CleaningExtraOption>>> GetCleaningExtraOption()
        {
            return await _context.CleaningExtraOption.ToListAsync();
        }

        // GET: api/CleaningExtraOptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CleaningExtraOption>> GetCleaningExtraOption(int id)
        {
            var cleaningExtraOption = await _context.CleaningExtraOption.FindAsync(id);

            if (cleaningExtraOption == null)
            {
                return NotFound();
            }

            return cleaningExtraOption;
        }

        [HttpGet("GetCleaningExtraOptionsByCategoryId")]
        public async Task<ActionResult<IEnumerable<CleaningExtraOption>>> GetCleaningExtraOptionsByCategoryId(int categoryId)
        {
            var cleaningExtraOption = await _context.CleaningExtraOption.Where(a => a.CleaningCategoryId == categoryId).ToListAsync();
            return cleaningExtraOption;
        }


        // PUT: api/CleaningExtraOptions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCleaningExtraOption(int id, CleaningExtraOption cleaningExtraOption)
        {
            if (id != cleaningExtraOption.Id)
            {
                return BadRequest();
            }

            _context.Entry(cleaningExtraOption).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CleaningExtraOptionExists(id))
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

        // POST: api/CleaningExtraOptions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CleaningExtraOption>> PostCleaningExtraOption(CleaningExtraOption cleaningExtraOption)
        {
            _context.CleaningExtraOption.Add(cleaningExtraOption);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCleaningExtraOption", new { id = cleaningExtraOption.Id }, cleaningExtraOption);
        }

        // DELETE: api/CleaningExtraOptions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CleaningExtraOption>> DeleteCleaningExtraOption(int id)
        {
            var cleaningExtraOption = await _context.CleaningExtraOption.FindAsync(id);
            if (cleaningExtraOption == null)
            {
                return NotFound();
            }

            _context.CleaningExtraOption.Remove(cleaningExtraOption);
            await _context.SaveChangesAsync();

            return cleaningExtraOption;
        }

        private bool CleaningExtraOptionExists(int id)
        {
            return _context.CleaningExtraOption.Any(e => e.Id == id);
        }
    }
}
