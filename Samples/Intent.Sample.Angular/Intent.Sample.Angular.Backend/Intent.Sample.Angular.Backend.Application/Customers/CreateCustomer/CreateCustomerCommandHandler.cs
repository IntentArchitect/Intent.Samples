using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Angular.Backend.Domain.Entities;
using Intent.Sample.Angular.Backend.Domain.Repositories;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandHandler", Version = "2.0")]

namespace Intent.Sample.Angular.Backend.Application.Customers.CreateCustomer
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Guid>
    {
        private readonly ICustomerRepository _customerRepository;

        [IntentManaged(Mode.Merge)]
        public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                TitleId = request.TitleId,
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                IsActive = request.IsActive,
                Addresses = request.Addresses
                    .Select(a => new Address
                    {
                        Line1 = a.Line1,
                        Line2 = a.Line2,
                        City = a.City,
                        Postal = a.Postal,
                        AddressType = a.AddressType
                    })
                    .ToList(),
                Loyalty = request.Loyalty is not null
                    ? new Loyalty
                    {
                        LoyaltyNo = request.Loyalty.LoyaltyNo,
                        Points = request.Loyalty.Points
                    }
                    : null
            };

            _customerRepository.Add(customer);
            await _customerRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return customer.Id;
        }
    }
}