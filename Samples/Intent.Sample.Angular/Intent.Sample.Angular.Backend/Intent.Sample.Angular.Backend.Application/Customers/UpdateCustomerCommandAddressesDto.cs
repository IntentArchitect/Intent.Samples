using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Angular.Backend.Domain;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Customers
{
    public class UpdateCustomerCommandAddressesDto
    {
        public UpdateCustomerCommandAddressesDto()
        {
            Line1 = null!;
            Line2 = null!;
            City = null!;
            Postal = null!;
        }

        public Guid Id { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string Postal { get; set; }
        public AddressType AddressType { get; set; }

        public static UpdateCustomerCommandAddressesDto Create(
            Guid id,
            string line1,
            string line2,
            string city,
            string postal,
            AddressType addressType)
        {
            return new UpdateCustomerCommandAddressesDto
            {
                Id = id,
                Line1 = line1,
                Line2 = line2,
                City = city,
                Postal = postal,
                AddressType = addressType
            };
        }
    }
}