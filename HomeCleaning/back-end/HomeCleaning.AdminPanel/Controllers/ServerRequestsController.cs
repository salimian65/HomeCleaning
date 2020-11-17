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
    public class ServerRequestsController : Controller
    {
        private readonly HomeCleaningContext _context;

        public ServerRequestsController(HomeCleaningContext context)
        {
            _context = context;
        }

        // GET: ServerRequests
        public async Task<IActionResult> Index()
        {
            var homeCleaningContext = _context.ServerRequest.Include(s => s.Order)
                                                            .Include(s => s.ServerUser)
                                                            .Include(s => s.Order.ClientUser).OrderBy(a=>a.OrderId);
            return View(await homeCleaningContext.ToListAsync());
        }

        // GET: ServerRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serverRequest = await _context.ServerRequest
                .Include(s => s.Order)
                .Include(s => s.ServerUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serverRequest == null)
            {
                return NotFound();
            }

            return View(serverRequest);
        }

        // GET: ServerRequests/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Order, "Id", "Id");
            ViewData["ServerUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: ServerRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServerUserId,OrderId,ServerRequestStatus,Id")] ServerRequest serverRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serverRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Order, "Id", "Id", serverRequest.OrderId);
            ViewData["ServerUserId"] = new SelectList(_context.Users, "Id", "Id", serverRequest.ServerUserId);
            return View(serverRequest);
        }

        // GET: ServerRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serverRequest = await _context.ServerRequest.FindAsync(id);
            if (serverRequest == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Order, "Id", "Id", serverRequest.OrderId);
            ViewData["ServerUserId"] = new SelectList(_context.Users, "Id", "Id", serverRequest.ServerUserId);
            return View(serverRequest);
        }

        // POST: ServerRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServerUserId,OrderId,ServerRequestStatus,Id")] ServerRequest serverRequest)
        {
            if (id != serverRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serverRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServerRequestExists(serverRequest.Id))
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
            ViewData["OrderId"] = new SelectList(_context.Order, "Id", "Id", serverRequest.OrderId);
            ViewData["ServerUserId"] = new SelectList(_context.Users, "Id", "Id", serverRequest.ServerUserId);
            return View(serverRequest);
        }

        // GET: ServerRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serverRequest = await _context.ServerRequest
                .Include(s => s.Order)
                .Include(s => s.ServerUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serverRequest == null)
            {
                return NotFound();
            }

            return View(serverRequest);
        }

        // POST: ServerRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serverRequest = await _context.ServerRequest.FindAsync(id);
            _context.ServerRequest.Remove(serverRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServerRequestExists(int id)
        {
            return _context.ServerRequest.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Accept(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serverRequest = await _context.ServerRequest.FindAsync(id);
            if (serverRequest == null)
            {
                return NotFound();
            }

            var serverRequestsForSpecifyOrder = _context.ServerRequest.Where(a => a.OrderId == serverRequest.OrderId).ToList();
            serverRequestsForSpecifyOrder.ForEach(a=>a.ServerRequestStatus=ServerRequestStatus.Rejected);
            serverRequest.ServerRequestStatus = ServerRequestStatus.Approved;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
