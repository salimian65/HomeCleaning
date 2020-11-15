using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HomeCleaning.Domain;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace HomeCleaning.Persistance
{
    public class ClaimsFactory : UserClaimsPrincipalFactory<ApplicationUser>
    {
        private readonly HomeCleaningContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ClaimsFactory(
            UserManager<ApplicationUser> userManager,
            IOptions<IdentityOptions> optionsAccessor,
            HomeCleaningContext context) : base(userManager, optionsAccessor)
        {
            _context = context;
            _userManager = userManager;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            identity.AddClaims(roles.Select(role => new Claim(JwtClaimTypes.Role, role)));

            return identity;
        }
    }
}
