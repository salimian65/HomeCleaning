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
    public class CleaningCategoriesController : Controller
    {
        private readonly HomeCleaningContext _context;

        public CleaningCategoriesController(HomeCleaningContext context)
        {
            _context = context;
        }

        // GET: CleaningCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.CleaningCategory.ToListAsync());
        }

        // GET: CleaningCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cleaningCategory = await _context.CleaningCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cleaningCategory == null)
            {
                return NotFound();
            }

            return View(cleaningCategory);
        }

        // GET: CleaningCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CleaningCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Id")] CleaningCategory cleaningCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cleaningCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cleaningCategory);
        }

        // GET: CleaningCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cleaningCategory = await _context.CleaningCategory.FindAsync(id);
            if (cleaningCategory == null)
            {
                return NotFound();
            }
            return View(cleaningCategory);
        }

        // POST: CleaningCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,Id")] CleaningCategory cleaningCategory)
        {
            if (id != cleaningCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cleaningCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CleaningCategoryExists(cleaningCategory.Id))
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
            return View(cleaningCategory);
        }

        // GET: CleaningCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cleaningCategory = await _context.CleaningCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cleaningCategory == null)
            {
                return NotFound();
            }

            return View(cleaningCategory);
        }

        // POST: CleaningCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cleaningCategory = await _context.CleaningCategory.FindAsync(id);
            _context.CleaningCategory.Remove(cleaningCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CleaningCategoryExists(int id)
        {
            return _context.CleaningCategory.Any(e => e.Id == id);
        }
    }
}
