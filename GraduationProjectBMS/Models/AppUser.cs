using Microsoft.AspNetCore.Identity;

namespace GraduationProjectBMS.Models
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<Article>? Articles { get; set; }
    }
}
