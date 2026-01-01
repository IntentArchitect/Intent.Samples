using AutoMapper;
using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Angular.Backend.Domain.Entities;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.AutoMapper.DtoMappingProfile", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Customers
{
    public class CustomerAddressDtoProfile : Profile
    {
        public CustomerAddressDtoProfile()
        {
            CreateMap<Address, CustomerAddressDto>();
        }
    }

    public static class CustomerAddressDtoMappingExtensions
    {
        public static CustomerAddressDto MapToCustomerAddressDto(this Address projectFrom, IMapper mapper) => mapper.Map<CustomerAddressDto>(projectFrom);

        public static List<CustomerAddressDto> MapToCustomerAddressDtoList(
            this IEnumerable<Address> projectFrom,
            IMapper mapper) => projectFrom.Select(x => x.MapToCustomerAddressDto(mapper)).ToList();
    }
}