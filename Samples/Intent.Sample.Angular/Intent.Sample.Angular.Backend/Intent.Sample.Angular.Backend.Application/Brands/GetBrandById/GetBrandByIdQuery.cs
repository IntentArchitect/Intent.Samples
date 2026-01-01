using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Angular.Backend.Application.Common.Interfaces;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryModels", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Brands.GetBrandById
{
    public class GetBrandByIdQuery : IRequest<BrandDto>, IQuery
    {
        public GetBrandByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}