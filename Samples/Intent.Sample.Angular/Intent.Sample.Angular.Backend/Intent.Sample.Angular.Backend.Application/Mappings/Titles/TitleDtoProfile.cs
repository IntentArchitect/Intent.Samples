using AutoMapper;
using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Angular.Backend.Domain.Entities;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.AutoMapper.DtoMappingProfile", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Titles
{
    public class TitleDtoProfile : Profile
    {
        public TitleDtoProfile()
        {
            CreateMap<Title, TitleDto>();
        }
    }

    public static class TitleDtoMappingExtensions
    {
        public static TitleDto MapToTitleDto(this Title projectFrom, IMapper mapper) => mapper.Map<TitleDto>(projectFrom);

        public static List<TitleDto> MapToTitleDtoList(this IEnumerable<Title> projectFrom, IMapper mapper) => projectFrom.Select(x => x.MapToTitleDto(mapper)).ToList();
    }
}