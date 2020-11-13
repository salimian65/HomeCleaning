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
    public class CleaningPackagesController : Controller
    {
        private readonly HomeCleaningContext _context;

        public CleaningPackagesController(HomeCleaningContext context)
        {
            _context = context;
        }

        // GET: CleaningPackages
        public async Task<IActionResult> Index()
        {
            var homeCleaningContext = _context.CleaningPackage.Include(c => c.CleaningCategory)
                .OrderBy(a => a.CleaningCategoryId); 
            return View(await homeCleaningContext.ToListAsync());
        }

        // GET: CleaningPackages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cleaningPackage = await _context.CleaningPackage
                .Include(c => c.CleaningCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cleaningPackage == null)
            {
                return NotFound();
            }

            return View(cleaningPackage);
        }

        // GET: CleaningPackages/Create
        public IActionResult Create()
        {
            ViewData["CleaningCategoryId"] = new SelectList(_context.CleaningCategory, "Id", "Name");
            return View();
        }

        // POST: CleaningPackages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,CleaningCategoryId,Id")] CleaningPackage cleaningPackage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cleaningPackage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CleaningCategoryId"] = new SelectList(_context.CleaningCategory, "Id", "Name", cleaningPackage.CleaningCategoryId);
            return View(cleaningPackage);
        }

        // GET: CleaningPackages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cleaningPackage = await _context.CleaningPackage.FindAsync(id);
            if (cleaningPackage == null)
            {
                return NotFound();
            }
            ViewData["CleaningCategoryId"] = new SelectList(_context.CleaningCategory, "Id", "Name", cleaningPackage.CleaningCategoryId);
            return View(cleaningPackage);
        }

        // POST: CleaningPackages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,CleaningCategoryId,Id")] CleaningPackage cleaningPackage)
        {
            if (id != cleaningPackage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cleaningPackage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CleaningPackageExists(cleaningPackage.Id))
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
            ViewData["CleaningCategoryId"] = new SelectList(_context.CleaningCategory, "Id", "Name", cleaningPackage.CleaningCategoryId);
            return View(cleaningPackage);
        }

        // GET: CleaningPackages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cleaningPackage = await _context.CleaningPackage
                .Include(c => c.CleaningCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cleaningPackage == null)
            {
                return NotFound();
            }

            return View(cleaningPackage);
        }

        // POST: CleaningPackages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cleaningPackage = await _context.CleaningPackage.FindAsync(id);
            _context.CleaningPackage.Remove(cleaningPackage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CleaningPackageExists(int id)
        {
            return _context.CleaningPackage.Any(e => e.Id == id);
        }
    }
}
