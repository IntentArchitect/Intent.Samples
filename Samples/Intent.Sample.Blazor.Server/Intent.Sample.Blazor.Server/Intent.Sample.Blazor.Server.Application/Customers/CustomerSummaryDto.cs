using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Application.Customers
{
    public record CustomerSummaryDto
    {
        public CustomerSummaryDto()
        {
            Name = null!;
            Surname = null!;
            Email = null!;
        }

        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Surname { get; init; }
        public string Email { get; init; }
        public bool IsActive { get; init; }
    }
}