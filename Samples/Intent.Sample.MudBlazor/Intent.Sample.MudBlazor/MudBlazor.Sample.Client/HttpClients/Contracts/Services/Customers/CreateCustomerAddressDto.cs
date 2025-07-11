using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.Blazor.HttpClients.DtoContract", Version = "2.0")]

namespace MudBlazor.Sample.Client.HttpClients.Contracts.Services.Customers
{
    public class CreateCustomerAddressDto
    {
        public CreateCustomerAddressDto()
        {
            Line1 = null!;
            City = null!;
            Country = null!;
            Postal = null!;
        }

        public string Line1 { get; set; }
        public string? Line2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Postal { get; set; }

        public static CreateCustomerAddressDto Create(string line1, string? line2, string city, string country, string postal)
        {
            return new CreateCustomerAddressDto
            {
                Line1 = line1,
                Line2 = line2,
                City = city,
                Country = country,
                Postal = postal
            };
        }
    }
}