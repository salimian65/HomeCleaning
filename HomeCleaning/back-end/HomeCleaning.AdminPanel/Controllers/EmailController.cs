using System.Threading.Tasks;
using HomeCleaning.AdminPanel.Models;
using HomeCleaning.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HomeCleaning.AdminPanel.Controllers
{
    public class EmailController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        public EmailController(UserManager<ApplicationUser> usrMgr)
        {
            userManager = usrMgr;
        }

        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return View("Error");

            var result = await userManager.ConfirmEmailAsync(user, token);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }
    }
}