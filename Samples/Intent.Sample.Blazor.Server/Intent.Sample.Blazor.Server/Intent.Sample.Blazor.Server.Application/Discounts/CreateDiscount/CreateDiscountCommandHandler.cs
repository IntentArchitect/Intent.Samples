using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Domain.Entities;
using Intent.Sample.Blazor.Server.Domain.Repositories;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandHandler", Version = "2.0")]

namespace Intent.Sample.Blazor.Server.Application.Discounts.CreateDiscount
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, Guid>
    {
        private readonly IDiscountRepository _discountRepository;

        [IntentManaged(Mode.Merge)]
        public CreateDiscountCommandHandler(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<Guid> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var discount = new Discount
            {
                Code = request.Code,
                DiscountAmount = request.DiscountAmount,
                Expiry = request.Expiry
            };

            _discountRepository.Add(discount);
            await _discountRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return discount.Id;
        }
    }
}