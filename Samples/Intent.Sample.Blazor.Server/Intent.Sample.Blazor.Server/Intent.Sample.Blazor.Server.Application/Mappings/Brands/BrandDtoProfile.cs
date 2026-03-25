using AutoMapper;
using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Domain.Entities;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.AutoMapper.DtoMappingProfile", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Application.Brands
{
    public class BrandDtoProfile : Profile
    {
        public BrandDtoProfile()
        {
            CreateMap<Brand, BrandDto>();
        }
    }

    public static class BrandDtoMappingExtensions
    {
        public static BrandDto MapToBrandDto(this Brand projectFrom, IMapper mapper) => mapper.Map<BrandDto>(projectFrom);

        public static List<BrandDto> MapToBrandDtoList(this IEnumerable<Brand> projectFrom, IMapper mapper) => projectFrom.Select(x => x.MapToBrandDto(mapper)).ToList();
    }
}