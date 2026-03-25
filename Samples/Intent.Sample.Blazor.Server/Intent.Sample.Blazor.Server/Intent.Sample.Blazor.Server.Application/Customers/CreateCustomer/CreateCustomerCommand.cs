using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Application.Common.Interfaces;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Application.Customers.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<Guid>, ICommand
    {
        public CreateCustomerCommand(Guid titleId,
            string name,
            string surname,
            string email,
            bool isActive,
            List<CreateCustomerCommandAddressesDto> addresses,
            CreateCustomerCommandLoyaltyDto? loyalty)
        {
            TitleId = titleId;
            Name = name;
            Surname = surname;
            Email = email;
            IsActive = isActive;
            Addresses = addresses;
            Loyalty = loyalty;
        }

        public Guid TitleId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public List<CreateCustomerCommandAddressesDto> Addresses { get; set; }
        public CreateCustomerCommandLoyaltyDto? Loyalty { get; set; }
    }
}