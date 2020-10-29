using Microsoft.AspNetCore.Authorization;

namespace HomeCleaning.AdminPanel.CustomPolicy
{
    public class AllowUserPolicy : IAuthorizationRequirement
    {
        public string[] AllowUsers { get; set; }

        public AllowUserPolicy(params string[] users)
        {
            AllowUsers = users;
        }
    }
}