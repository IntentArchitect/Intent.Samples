using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Application.Common.Interfaces;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Application.Customers.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest, ICommand
    {
        public UpdateCustomerCommand(Guid id,
            Guid titleId,
            string name,
            string surname,
            string email,
            bool isActive,
            List<UpdateCustomerCommandAddressesDto> addresses,
            UpdateCustomerCommandLoyaltyDto? loyalty)
        {
            Id = id;
            TitleId = titleId;
            Name = name;
            Surname = surname;
            Email = email;
            IsActive = isActive;
            Addresses = addresses;
            Loyalty = loyalty;
        }

        public Guid Id { get; set; }
        public Guid TitleId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public List<UpdateCustomerCommandAddressesDto> Addresses { get; set; }
        public UpdateCustomerCommandLoyaltyDto? Loyalty { get; set; }
    }
}