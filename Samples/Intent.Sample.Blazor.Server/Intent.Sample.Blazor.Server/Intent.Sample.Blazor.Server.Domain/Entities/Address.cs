using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace Intent.Sample.Blazor.Server.Domain.Entities
{
    public class Address
    {
        public Address()
        {
            Line1 = null!;
            Line2 = null!;
            City = null!;
            Postal = null!;
        }

        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string City { get; set; }

        public string Postal { get; set; }

        public AddressType AddressType { get; set; }
    }
}