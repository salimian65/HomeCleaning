using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeCleaning.Domain;
using HomeCleaning.Persistance;
using HomeCleaning.Persistance.DataAccess;

namespace HomeCleaning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpaceSizesController : ControllerBase
    {
        private readonly HomeCleaningContext _context;

        public SpaceSizesController(HomeCleaningContext context)
        {
            _context = context;
        }

        // GET: api/SpaceSizes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpaceSize>>> GetSpaceSize()
        {
            return await _context.SpaceSize.ToListAsync();
        }

        // GET: api/SpaceSizes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SpaceSize>> GetSpaceSize(int id)
        {
            var spaceSize = await _context.SpaceSize.FindAsync(id);

            if (spaceSize == null)
            {
                return NotFound();
            }

            return spaceSize;
        }

        [HttpGet("GetSpaceSizesByCategoryId")]
        public async Task<ActionResult<IEnumerable<SpaceSize>>> GetSpaceSizesByCategoryId(int categoryId)
        {
            var spaceSize = await _context.SpaceSize.Where(a => a.CleaningCategoryId == categoryId).ToListAsync();
            return spaceSize;
        }
        
        // PUT: api/SpaceSizes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpaceSize(int id, SpaceSize spaceSize)
        {
            if (id != spaceSize.Id)
            {
                return BadRequest();
            }

            _context.Entry(spaceSize).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpaceSizeExists(id))
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

        // POST: api/SpaceSizes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SpaceSize>> PostSpaceSize(SpaceSize spaceSize)
        {
            _context.SpaceSize.Add(spaceSize);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpaceSize", new { id = spaceSize.Id }, spaceSize);
        }

        // DELETE: api/SpaceSizes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SpaceSize>> DeleteSpaceSize(int id)
        {
            var spaceSize = await _context.SpaceSize.FindAsync(id);
            if (spaceSize == null)
            {
                return NotFound();
            }

            _context.SpaceSize.Remove(spaceSize);
            await _context.SaveChangesAsync();

            return spaceSize;
        }

        private bool SpaceSizeExists(int id)
        {
            return _context.SpaceSize.Any(e => e.Id == id);
        }
    }
}
