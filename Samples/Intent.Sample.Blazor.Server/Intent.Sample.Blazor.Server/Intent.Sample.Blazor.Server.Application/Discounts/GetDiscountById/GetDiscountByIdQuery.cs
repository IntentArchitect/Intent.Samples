using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Application.Common.Interfaces;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryModels", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Application.Discounts.GetDiscountById
{
    public class GetDiscountByIdQuery : IRequest<DiscountDto>, IQuery
    {
        public GetDiscountByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}