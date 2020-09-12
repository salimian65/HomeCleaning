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
        public async Task<ActionResult<IEnumerable<CleaningOption>>> GetCleaningOption()
        {
            return await _context.CleaningOption.ToListAsync();
        }

        // GET: api/CleaningOptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CleaningOption>> GetCleaningOption(int id)
        {
            var cleaningOption = await _context.CleaningOption.FindAsync(id);

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
        public async Task<IActionResult> PutCleaningOption(int id, CleaningOption cleaningOption)
        {
            if (id != cleaningOption.Id)
            {
                return BadRequest();
            }

            _context.Entry(cleaningOption).State = EntityState.Modified;

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
        public async Task<ActionResult<CleaningOption>> PostCleaningOption(CleaningOption cleaningOption)
        {
            _context.CleaningOption.Add(cleaningOption);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCleaningOption", new { id = cleaningOption.Id }, cleaningOption);
        }

        // DELETE: api/CleaningOptions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CleaningOption>> DeleteCleaningOption(int id)
        {
            var cleaningOption = await _context.CleaningOption.FindAsync(id);
            if (cleaningOption == null)
            {
                return NotFound();
            }

            _context.CleaningOption.Remove(cleaningOption);
            await _context.SaveChangesAsync();

            return cleaningOption;
        }

        private bool CleaningOptionExists(int id)
        {
            return _context.CleaningOption.Any(e => e.Id == id);
        }
    }
}
