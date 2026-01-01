using AutoMapper;
using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Angular.Backend.Domain.Repositories;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryHandler", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Discounts.GetDiscounts
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class GetDiscountsQueryHandler : IRequestHandler<GetDiscountsQuery, List<DiscountDto>>
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;

        [IntentManaged(Mode.Merge)]
        public GetDiscountsQueryHandler(IDiscountRepository discountRepository, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<List<DiscountDto>> Handle(GetDiscountsQuery request, CancellationToken cancellationToken)
        {
            var discounts = await _discountRepository.FindAllAsync(cancellationToken);
            return discounts.MapToDiscountDtoList(_mapper);
        }
    }
}