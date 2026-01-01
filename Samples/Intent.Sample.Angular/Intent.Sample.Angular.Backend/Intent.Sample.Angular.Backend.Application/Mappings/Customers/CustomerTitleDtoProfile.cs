using AutoMapper;
using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Angular.Backend.Domain.Entities;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.AutoMapper.DtoMappingProfile", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Customers
{
    public class CustomerTitleDtoProfile : Profile
    {
        public CustomerTitleDtoProfile()
        {
            CreateMap<Title, CustomerTitleDto>();
        }
    }

    public static class CustomerTitleDtoMappingExtensions
    {
        public static CustomerTitleDto MapToCustomerTitleDto(this Title projectFrom, IMapper mapper) => mapper.Map<CustomerTitleDto>(projectFrom);

        public static List<CustomerTitleDto> MapToCustomerTitleDtoList(this IEnumerable<Title> projectFrom, IMapper mapper) => projectFrom.Select(x => x.MapToCustomerTitleDto(mapper)).ToList();
    }
}