using IdentityModel;
using IdentityServer4.Models;

namespace HomeCleaning.IdentityProvider2
{
    public sealed class ProfileWithIdentityResource : IdentityResources.Profile
    {
        public ProfileWithIdentityResource()
        {
            this.UserClaims.Add(JwtClaimTypes.Role);
        }
    }
}
