using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Domain;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Application.Customers
{
    public record UpdateCustomerCommandAddressesDto
    {
        public UpdateCustomerCommandAddressesDto()
        {
            Line1 = null!;
            Line2 = null!;
            City = null!;
            Postal = null!;
        }

        public Guid Id { get; init; }
        public string Line1 { get; init; }
        public string Line2 { get; init; }
        public string City { get; init; }
        public string Postal { get; init; }
        public AddressType AddressType { get; init; }
    }
}