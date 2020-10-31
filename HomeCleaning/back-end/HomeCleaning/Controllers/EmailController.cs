using System.Threading.Tasks;
using HomeCleaning.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HomeCleaning.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private UserManager<ApplicationUser> userManager;
        public EmailController(UserManager<ApplicationUser> usrMgr)
        {
            userManager = usrMgr;
        }

        [HttpGet]
        public async Task<object> ConfirmEmail( string email, string token)
        {
            var user = await userManager.FindByEmailAsync(email);
            //  if (user == null)
            //     return View("Error");

            var result = await userManager.ConfirmEmailAsync(user, token);
            return new ActionResult<IdentityResult>(result);
        }
    }
}
