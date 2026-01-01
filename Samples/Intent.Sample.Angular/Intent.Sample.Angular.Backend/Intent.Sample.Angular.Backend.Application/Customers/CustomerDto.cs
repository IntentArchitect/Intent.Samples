using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Customers
{
    public class CustomerDto
    {
        public CustomerDto()
        {
            Name = null!;
            Surname = null!;
            Email = null!;
            Title = null!;
            Addresses = null!;
        }

        public Guid Id { get; set; }
        public Guid TitleId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public CustomerTitleDto Title { get; set; }
        public List<CustomerAddressDto> Addresses { get; set; }
        public CustomerLoyaltyDto? Loyalty { get; set; }

        public static CustomerDto Create(
            Guid id,
            Guid titleId,
            string name,
            string surname,
            string email,
            bool isActive,
            CustomerTitleDto title,
            List<CustomerAddressDto> addresses,
            CustomerLoyaltyDto? loyalty)
        {
            return new CustomerDto
            {
                Id = id,
                TitleId = titleId,
                Name = name,
                Surname = surname,
                Email = email,
                IsActive = isActive,
                Title = title,
                Addresses = addresses,
                Loyalty = loyalty
            };
        }
    }
}