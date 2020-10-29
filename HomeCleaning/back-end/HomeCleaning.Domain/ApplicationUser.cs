using Microsoft.AspNetCore.Identity;

namespace HomeCleaning.Domain
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Cellphone { get; set; }
        /*public Country Country { get; set; }

     public int Age { get; set; }

     [Required]
     public string Salary { get; set; }*/
    }
}
