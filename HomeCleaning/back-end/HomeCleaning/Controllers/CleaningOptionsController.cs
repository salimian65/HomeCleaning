using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeCleaning.Domain;
using HomeCleaning.Persistance;

namespace HomeCleaning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CleaningOptionsController : ControllerBase
    {
        private readonly HomeCleaningContext _context;

        public CleaningOptionsController(HomeCleaningContext context)
        {
            _context = context;
        }

        // GET: api/CleaningOptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CleaningExtraOption>>> GetCleaningOption()
        {
            return await _context.CleaningExtraOption.ToListAsync();
        }

        // GET: api/CleaningOptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CleaningExtraOption>> GetCleaningOption(int id)
        {
            var cleaningOption = await _context.CleaningExtraOption.FindAsync(id);

            if (cleaningOption == null)
            {
                return NotFound();
            }

            return cleaningOption;
        }

        // PUT: api/CleaningOptions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCleaningOption(int id, CleaningExtraOption cleaningExtraOption)
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
                if (!CleaningOptionExists(id))
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

        // POST: api/CleaningOptions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CleaningExtraOption>> PostCleaningOption(CleaningExtraOption cleaningExtraOption)
        {
            _context.CleaningExtraOption.Add(cleaningExtraOption);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCleaningOption", new { id = cleaningExtraOption.Id }, cleaningExtraOption);
        }

        // DELETE: api/CleaningOptions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CleaningExtraOption>> DeleteCleaningOption(int id)
        {
            var cleaningOption = await _context.CleaningExtraOption.FindAsync(id);
            if (cleaningOption == null)
            {
                return NotFound();
            }

            _context.CleaningExtraOption.Remove(cleaningOption);
            await _context.SaveChangesAsync();

            return cleaningOption;
        }

        private bool CleaningOptionExists(int id)
        {
            return _context.CleaningExtraOption.Any(e => e.Id == id);
        }
    }
}
