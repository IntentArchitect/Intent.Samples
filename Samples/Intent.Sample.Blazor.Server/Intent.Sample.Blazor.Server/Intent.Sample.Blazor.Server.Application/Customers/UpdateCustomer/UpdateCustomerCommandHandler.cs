using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Domain.Common;
using Intent.Sample.Blazor.Server.Domain.Common.Exceptions;
using Intent.Sample.Blazor.Server.Domain.Entities;
using Intent.Sample.Blazor.Server.Domain.Repositories;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandHandler", Version = "2.0")]

namespace Intent.Sample.Blazor.Server.Application.Customers.UpdateCustomer
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;

        [IntentManaged(Mode.Merge)]
        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.FindByIdAsync(request.Id, cancellationToken);
            if (customer is null)
            {
                throw new NotFoundException($"Could not find Customer '{request.Id}'");
            }

            customer.TitleId = request.TitleId;
            customer.Name = request.Name;
            customer.Surname = request.Surname;
            customer.Email = request.Email;
            customer.IsActive = request.IsActive;
            customer.Addresses = UpdateHelper.CreateOrUpdateCollection(customer.Addresses, request.Addresses, (e, d) => e.Id == d.Id, CreateOrUpdateAddress);
            if (request.Loyalty != null)
            {
                customer.Loyalty ??= new Loyalty();
                customer.Loyalty.Id = request.Loyalty.Id;
                customer.Loyalty.LoyaltyNo = request.Loyalty.LoyaltyNo;
                customer.Loyalty.Points = request.Loyalty.Points;
            }
            else
            {
                customer.Loyalty = null;
            }
        }

        [IntentManaged(Mode.Fully)]
        private static Address CreateOrUpdateAddress(Address? entity, UpdateCustomerCommandAddressesDto dto)
        {
            entity ??= new Address();
            entity.Id = dto.Id;
            entity.Line1 = dto.Line1;
            entity.Line2 = dto.Line2;
            entity.City = dto.City;
            entity.Postal = dto.Postal;
            entity.AddressType = dto.AddressType;
            return entity;
        }
    }
}