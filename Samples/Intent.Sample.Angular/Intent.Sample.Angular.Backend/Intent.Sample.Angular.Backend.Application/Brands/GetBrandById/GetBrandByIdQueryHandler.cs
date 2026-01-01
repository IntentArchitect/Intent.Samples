using AutoMapper;
using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Angular.Backend.Domain.Common.Exceptions;
using Intent.Sample.Angular.Backend.Domain.Repositories;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryHandler", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Brands.GetBrandById
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, BrandDto>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        [IntentManaged(Mode.Merge)]
        public GetBrandByIdQueryHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<BrandDto> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            var brand = await _brandRepository.FindByIdAsync(request.Id, cancellationToken);
            if (brand is null)
            {
                throw new NotFoundException($"Could not find Brand '{request.Id}'");
            }
            return brand.MapToBrandDto(_mapper);
        }
    }
}