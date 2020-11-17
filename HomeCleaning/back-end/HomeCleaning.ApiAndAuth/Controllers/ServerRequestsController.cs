using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Domain;
using HomeCleaning.Domain;
using HomeCleaning.Persistance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeCleaning.ApiAndAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServerRequestsController : ControllerBase
    {
        private readonly IUserContext _userContext;
        private readonly HomeCleaningContext _context;

        public ServerRequestsController(HomeCleaningContext context, IUserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        // GET: api/ServerRequests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServerRequest>>> GetServerRequest()
        {
            return await _context.ServerRequest.ToListAsync();
        }

        // GET: api/ServerRequests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServerRequest>> GetServerRequest(int id)
        {
            var serverRequest = await _context.ServerRequest.FindAsync(id);

            if (serverRequest == null)
            {
                return NotFound();
            }

            return serverRequest;
        }

        // PUT: api/ServerRequests/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServerRequest(int id, ServerRequest serverRequest)
        {
            if (id != serverRequest.Id)
            {
                return BadRequest();
            }

            _context.Entry(serverRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServerRequestExists(id))
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

        // [Authorize(Roles = "server")]
        // [Authorize]
        [HttpPost()]
        public async Task<ActionResult<ServerRequest>> PostServerRequest([FromBody] int orderId)
        {
            var serverRequest = new ServerRequest
            {
                OrderId = orderId,
                ServerUserId = "2311608c-3b69-4cad-8fe2-ea180caaff2d",// MAJID  //  "87b294fd-cb1a-4f13-b5bc-82fe745d663a", // elham
                //      _userContext.CurrentUserPrincipal.UserId.ToString(),
                ServerRequestStatus = ServerRequestStatus.ServerRequested
            };

            _context.ServerRequest.Add(serverRequest);
            var order = await _context.Order.FindAsync(orderId);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetServerRequest", new { id = serverRequest.Id }, serverRequest);
        }

        // DELETE: api/ServerRequests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServerRequest>> DeleteServerRequest(int id)
        {
            var serverRequest = await _context.ServerRequest.FindAsync(id);
            if (serverRequest == null)
            {
                return NotFound();
            }

            _context.ServerRequest.Remove(serverRequest);
            await _context.SaveChangesAsync();

            return serverRequest;
        }

        private bool ServerRequestExists(int id)
        {
            return _context.ServerRequest.Any(e => e.Id == id);
        }
    }
}
