using Microsoft.AspNetCore.Identity;

namespace Movies.Authentication
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool IsDeleted { get; set; } = false;

        public string YourFavirotePerson { get; set; }

    }
}
