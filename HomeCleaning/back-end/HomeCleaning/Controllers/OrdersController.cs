using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Domain;
using HomeCleaning.Domain;
using HomeCleaning.Persistance;
using HomeCleaning.Persistance.DataAccess;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeCleaning.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IUserContext _userContext;
        private readonly HomeCleaningContext _context;

        public OrdersController(HomeCleaningContext context, IUserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
        {
            return await _context.Order
                .Include(a => a.ClientUser)
                .Include(a => a.CleaningExtraOption)
                .Include(a => a.CleaningPackage)
                .Include(a => a.SpaceSize)
                .ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Order.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/Orders
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //  [Authorize(Roles = "customer")]
        //  [Authorize]
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            order.RegisterTime = DateTime.Now;
            order.ScheduledTime = DateTime.Now;
            var UserName = User.FindFirst(c => c.Type == JwtClaimTypes.Name && c.Issuer == "http://localhost:5000").Value;
            var dd = _userContext.CurrentUserPrincipal.UserId;
            order.ClientUserId = "8a1c5263-f8ad-405b-a25d-707ce4dd32b2"; // _userContext.CurrentUserPrincipal.UserId;
            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();

            return order;
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}
