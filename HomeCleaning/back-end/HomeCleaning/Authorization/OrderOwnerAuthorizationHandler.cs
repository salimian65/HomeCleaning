using System.Threading.Tasks;
using HomeCleaning.Domain;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;

namespace HomeCleaning.Api.Authorization {
    public class OrderOwnerAuthorizationHandler : AuthorizationHandler<OrderOwnerAuthorizationRequirement, Order> {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OrderOwnerAuthorizationRequirement requirement, Order resource) 
        {
            if (!context.User.HasClaim(c => c.Type == JwtClaimTypes.Name && c.Issuer == "http://localhost:5000"))
                return Task.CompletedTask;

            var userName = context.User.FindFirst(c => c.Type == JwtClaimTypes.Name && c.Issuer == "http://localhost:5000").Value;

            //if (userName == resource.UserName)
            //    context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
