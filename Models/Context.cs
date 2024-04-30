using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Movies.Authentication;

namespace Movies.Models
{
    public class Context:IdentityDbContext<AppUser>
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        public Context() : base() { }
    }
}
