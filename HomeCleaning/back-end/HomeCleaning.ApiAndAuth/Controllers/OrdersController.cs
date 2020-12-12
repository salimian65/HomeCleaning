using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Domain;
using HomeCleaning.Domain;
using HomeCleaning.Persistance;
using IdentityServer4;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeCleaning.ApiAndAuth.Controllers
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
                .Include(a=>a.ClientUser)
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
        // [Authorize]
        // [Authorize(Roles = "customer")]
        //[Authorize(IdentityServerConstants.LocalApi.PolicyName)]
        // [Authorize(Policy = "Customer")]
       // [Authorize(Policy = "ProductOwner")]
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            order.RegisterTime = DateTime.Now;
            order.OrderStatus = OrderStatus.CoustomerRequested;
           // order.ScheduledTime = DateTime.Now;
            order.ClientUserId = "418afdbb-b0c3-4843-b790-bfbc903aef1b"; // Mohammad  "34702bf7-2362-417a-b9c0-047e88210b38"; //mehrdad
           // product.UserName = User.FindFirst(c => c.Type == JwtClaimTypes.Name && c.Issuer == "http://localhost:5000").Value;
            // User.Claims.Select(c => new { c.Type, c.Value }).ToArray();
            // _userContext.CurrentUserPrincipal.UserId;
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
