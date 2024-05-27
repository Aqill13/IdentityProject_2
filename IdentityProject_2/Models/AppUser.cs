using Microsoft.AspNetCore.Identity;

namespace IdentityProject_2.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
