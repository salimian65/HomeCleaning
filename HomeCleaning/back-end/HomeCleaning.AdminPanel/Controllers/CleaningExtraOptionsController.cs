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
    public class CleaningExtraOptionsController : Controller
    {
        private readonly HomeCleaningContext _context;

        public CleaningExtraOptionsController(HomeCleaningContext context)
        {
            _context = context;
        }

        // GET: CleaningExtraOptions
        public async Task<IActionResult> Index()
        {
            var homeCleaningContext = _context.CleaningExtraOption.Include(c => c.CleaningCategory)
                .OrderBy(a => a.CleaningCategoryId); ;
            return View(await homeCleaningContext.ToListAsync());
        }

        // GET: CleaningExtraOptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cleaningExtraOption = await _context.CleaningExtraOption
                .Include(c => c.CleaningCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cleaningExtraOption == null)
            {
                return NotFound();
            }

            return View(cleaningExtraOption);
        }

        // GET: CleaningExtraOptions/Create
        public IActionResult Create()
        {
            ViewData["CleaningCategoryId"] = new SelectList(_context.CleaningCategory, "Id", "Name");
            return View();
        }

        // POST: CleaningExtraOptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,CleaningCategoryId,Id")] CleaningExtraOption cleaningExtraOption)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cleaningExtraOption);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CleaningCategoryId"] = new SelectList(_context.CleaningCategory, "Id", "Name", cleaningExtraOption.CleaningCategoryId);
            return View(cleaningExtraOption);
        }

        // GET: CleaningExtraOptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cleaningExtraOption = await _context.CleaningExtraOption.FindAsync(id);
            if (cleaningExtraOption == null)
            {
                return NotFound();
            }
            ViewData["CleaningCategoryId"] = new SelectList(_context.CleaningCategory, "Id", "Name", cleaningExtraOption.CleaningCategoryId);
            return View(cleaningExtraOption);
        }

        // POST: CleaningExtraOptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,CleaningCategoryId,Id")] CleaningExtraOption cleaningExtraOption)
        {
            if (id != cleaningExtraOption.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cleaningExtraOption);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CleaningExtraOptionExists(cleaningExtraOption.Id))
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
            ViewData["CleaningCategoryId"] = new SelectList(_context.CleaningCategory, "Id", "Name", cleaningExtraOption.CleaningCategoryId);
            return View(cleaningExtraOption);
        }

        // GET: CleaningExtraOptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cleaningExtraOption = await _context.CleaningExtraOption
                .Include(c => c.CleaningCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cleaningExtraOption == null)
            {
                return NotFound();
            }

            return View(cleaningExtraOption);
        }

        // POST: CleaningExtraOptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cleaningExtraOption = await _context.CleaningExtraOption.FindAsync(id);
            _context.CleaningExtraOption.Remove(cleaningExtraOption);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CleaningExtraOptionExists(int id)
        {
            return _context.CleaningExtraOption.Any(e => e.Id == id);
        }
    }
}
