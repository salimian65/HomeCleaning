using System;
using System.Linq;
using System.Security.Claims;
using Framework.Domain;
using Microsoft.AspNetCore.Http;

namespace HomeCleaning.Api.Contexts
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor contextAccessor;

        public UserContext(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        public UserPrincipalDto CurrentUserPrincipal => new UserPrincipalDto(Guid.Parse(contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value),
            contextAccessor.HttpContext.User.FindFirst("preferred_username").Value,
            Convert.ToInt32(contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "org_info_id")?.Value));
    }
}