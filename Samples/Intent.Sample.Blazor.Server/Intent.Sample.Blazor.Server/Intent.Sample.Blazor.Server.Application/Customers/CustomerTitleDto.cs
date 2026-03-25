using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Application.Customers
{
    public record CustomerTitleDto
    {
        public CustomerTitleDto()
        {
            Name = null!;
        }

        public Guid Id { get; init; }
        public string Name { get; init; }
    }
}