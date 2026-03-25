using AutoMapper;
using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Domain.Common.Exceptions;
using Intent.Sample.Blazor.Server.Domain.Repositories;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryHandler", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Application.Discounts.GetDiscountById
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class GetDiscountByIdQueryHandler : IRequestHandler<GetDiscountByIdQuery, DiscountDto>
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;

        [IntentManaged(Mode.Merge)]
        public GetDiscountByIdQueryHandler(IDiscountRepository discountRepository, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<DiscountDto> Handle(GetDiscountByIdQuery request, CancellationToken cancellationToken)
        {
            var discount = await _discountRepository.FindByIdAsync(request.Id, cancellationToken);
            if (discount is null)
            {
                throw new NotFoundException($"Could not find Discount '{request.Id}'");
            }
            return discount.MapToDiscountDto(_mapper);
        }
    }
}