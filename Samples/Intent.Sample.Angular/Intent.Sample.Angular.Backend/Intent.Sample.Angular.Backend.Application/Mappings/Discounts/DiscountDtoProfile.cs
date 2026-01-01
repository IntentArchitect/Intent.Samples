using AutoMapper;
using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Angular.Backend.Domain.Entities;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.AutoMapper.DtoMappingProfile", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Discounts
{
    public class DiscountDtoProfile : Profile
    {
        public DiscountDtoProfile()
        {
            CreateMap<Discount, DiscountDto>();
        }
    }

    public static class DiscountDtoMappingExtensions
    {
        public static DiscountDto MapToDiscountDto(this Discount projectFrom, IMapper mapper) => mapper.Map<DiscountDto>(projectFrom);

        public static List<DiscountDto> MapToDiscountDtoList(this IEnumerable<Discount> projectFrom, IMapper mapper) => projectFrom.Select(x => x.MapToDiscountDto(mapper)).ToList();
    }
}