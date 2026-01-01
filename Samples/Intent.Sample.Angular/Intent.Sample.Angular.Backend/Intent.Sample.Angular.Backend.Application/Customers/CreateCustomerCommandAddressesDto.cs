using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Angular.Backend.Domain;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Customers
{
    public class CreateCustomerCommandAddressesDto
    {
        public CreateCustomerCommandAddressesDto()
        {
            Line1 = null!;
            Line2 = null!;
            City = null!;
            Postal = null!;
        }

        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string Postal { get; set; }
        public AddressType AddressType { get; set; }

        public static CreateCustomerCommandAddressesDto Create(
            string line1,
            string line2,
            string city,
            string postal,
            AddressType addressType)
        {
            return new CreateCustomerCommandAddressesDto
            {
                Line1 = line1,
                Line2 = line2,
                City = city,
                Postal = postal,
                AddressType = addressType
            };
        }
    }
}