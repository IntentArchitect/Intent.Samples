using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Angular.Backend.Application.Common.Interfaces;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryModels", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Brands.GetBrands
{
    public class GetBrandsQuery : IRequest<List<BrandDto>>, IQuery
    {
        public GetBrandsQuery()
        {
        }
    }
}