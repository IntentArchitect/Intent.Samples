using AutoMapper;
using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Angular.Backend.Domain.Repositories;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryHandler", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Brands.GetBrands
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class GetBrandsQueryHandler : IRequestHandler<GetBrandsQuery, List<BrandDto>>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        [IntentManaged(Mode.Merge)]
        public GetBrandsQueryHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<List<BrandDto>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
        {
            var brands = await _brandRepository.FindAllAsync(cancellationToken);
            return brands.MapToBrandDtoList(_mapper);
        }
    }
}