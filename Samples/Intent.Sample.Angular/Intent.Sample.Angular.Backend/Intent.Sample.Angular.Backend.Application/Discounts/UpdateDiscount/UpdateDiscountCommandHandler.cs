using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Angular.Backend.Domain.Common.Exceptions;
using Intent.Sample.Angular.Backend.Domain.Repositories;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandHandler", Version = "2.0")]

namespace Intent.Sample.Angular.Backend.Application.Discounts.UpdateDiscount
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class UpdateDiscountCommandHandler : IRequestHandler<UpdateDiscountCommand>
    {
        private readonly IDiscountRepository _discountRepository;

        [IntentManaged(Mode.Merge)]
        public UpdateDiscountCommandHandler(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {
            var discount = await _discountRepository.FindByIdAsync(request.Id, cancellationToken);
            if (discount is null)
            {
                throw new NotFoundException($"Could not find Discount '{request.Id}'");
            }

            discount.Code = request.Code;
            discount.DiscountAmount = request.DiscountAmount;
            discount.Expiry = request.Expiry;
        }
    }
}