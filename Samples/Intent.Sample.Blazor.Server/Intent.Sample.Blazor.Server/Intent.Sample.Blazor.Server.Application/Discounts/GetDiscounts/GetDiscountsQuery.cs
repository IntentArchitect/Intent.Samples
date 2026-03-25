using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Application.Common.Interfaces;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryModels", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Application.Discounts.GetDiscounts
{
    public class GetDiscountsQuery : IRequest<List<DiscountDto>>, IQuery
    {
        public GetDiscountsQuery()
        {
        }
    }
}