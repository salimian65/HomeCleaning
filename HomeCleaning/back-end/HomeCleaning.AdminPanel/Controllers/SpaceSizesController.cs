using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HomeCleaning.Domain;
using HomeCleaning.Persistance;

namespace HomeCleaning.AdminPanel.Controllers
{
    public class SpaceSizesController : Controller
    {
        private readonly HomeCleaningContext _context;

        public SpaceSizesController(HomeCleaningContext context)
        {
            _context = context;
        }

        // GET: SpaceSizes
        public async Task<IActionResult> Index()
        {
            var homeCleaningContext = _context.SpaceSize.Include(s => s.CleaningCategory)
                                                        .OrderBy(a=>a.CleaningCategoryId);
            return View(await homeCleaningContext.ToListAsync());
        }

        // GET: SpaceSizes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spaceSize = await _context.SpaceSize
                .Include(s => s.CleaningCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spaceSize == null)
            {
                return NotFound();
            }

            return View(spaceSize);
        }

        // GET: SpaceSizes/Create
        public IActionResult Create()
        {
            ViewData["CleaningCategoryId"] = new SelectList(_context.CleaningCategory, "Id", "Name");
            return View();
        }

        // POST: SpaceSizes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,CleaningCategoryId,Id")] SpaceSize spaceSize)
        {
            if (ModelState.IsValid)
            {
                _context.Add(spaceSize);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CleaningCategoryId"] = new SelectList(_context.CleaningCategory, "Id", "Name", spaceSize.CleaningCategoryId);
            return View(spaceSize);
        }

        // GET: SpaceSizes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spaceSize = await _context.SpaceSize.FindAsync(id);
            if (spaceSize == null)
            {
                return NotFound();
            }
            ViewData["CleaningCategoryId"] = new SelectList(_context.CleaningCategory, "Id", "Name", spaceSize.CleaningCategoryId);
            return View(spaceSize);
        }

        // POST: SpaceSizes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,CleaningCategoryId,Id")] SpaceSize spaceSize)
        {
            if (id != spaceSize.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(spaceSize);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpaceSizeExists(spaceSize.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CleaningCategoryId"] = new SelectList(_context.CleaningCategory, "Id", "Name", spaceSize.CleaningCategoryId);
            return View(spaceSize);
        }

        // GET: SpaceSizes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spaceSize = await _context.SpaceSize
                .Include(s => s.CleaningCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spaceSize == null)
            {
                return NotFound();
            }

            return View(spaceSize);
        }

        // POST: SpaceSizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var spaceSize = await _context.SpaceSize.FindAsync(id);
            _context.SpaceSize.Remove(spaceSize);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpaceSizeExists(int id)
        {
            return _context.SpaceSize.Any(e => e.Id == id);
        }
    }
}
