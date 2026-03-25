using System.Security.Claims;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Identity.CurrentUserInterface", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Application.Common.Interfaces
{
    public interface ICurrentUser
    {
        string? Id { get; }
        string? Name { get; }
        ClaimsPrincipal Principal { get; }
    }
}