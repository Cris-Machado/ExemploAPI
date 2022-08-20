using Microsoft.AspNetCore.Identity;

namespace PointNow.API.Identity.Entitites
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
    }
}
