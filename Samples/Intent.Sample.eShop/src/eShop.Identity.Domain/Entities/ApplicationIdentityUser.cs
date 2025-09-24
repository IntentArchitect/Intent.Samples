using Intent.RoslynWeaver.Attributes;
using Microsoft.AspNetCore.Identity;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace eShop.Identity.Domain.Entities
{
    public class ApplicationIdentityUser : IdentityUser<string>
    {
        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpired { get; set; }
    }
}