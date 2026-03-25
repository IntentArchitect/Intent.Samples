using AutoMapper;
using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Domain.Entities;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.AutoMapper.DtoMappingProfile", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Application.Customers
{
    public class CustomerDtoProfile : Profile
    {
        public CustomerDtoProfile()
        {
            CreateMap<Customer, CustomerDto>()
                .ForMember(d => d.Addresses, opt => opt.MapFrom(src => src.Addresses));
        }
    }

    public static class CustomerDtoMappingExtensions
    {
        public static CustomerDto MapToCustomerDto(this Customer projectFrom, IMapper mapper) => mapper.Map<CustomerDto>(projectFrom);

        public static List<CustomerDto> MapToCustomerDtoList(this IEnumerable<Customer> projectFrom, IMapper mapper) => projectFrom.Select(x => x.MapToCustomerDto(mapper)).ToList();
    }
}