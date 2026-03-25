using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Application.Customers
{
    public record CustomerDto
    {
        public CustomerDto()
        {
            Name = null!;
            Surname = null!;
            Email = null!;
            Title = null!;
            Addresses = null!;
        }

        public Guid Id { get; init; }
        public Guid TitleId { get; init; }
        public string Name { get; init; }
        public string Surname { get; init; }
        public string Email { get; init; }
        public bool IsActive { get; init; }
        public CustomerTitleDto Title { get; init; }
        public List<CustomerAddressDto> Addresses { get; init; }
        public CustomerLoyaltyDto? Loyalty { get; init; }
    }
}