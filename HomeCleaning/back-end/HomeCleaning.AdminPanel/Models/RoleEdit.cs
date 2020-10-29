using System.Collections.Generic;
using HomeCleaning.Domain;
using Microsoft.AspNetCore.Identity;

namespace HomeCleaning.AdminPanel.Models
{
    public class RoleEdit
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<ApplicationUser> Members { get; set; }
        public IEnumerable<ApplicationUser> NonMembers { get; set; }
    }
}
