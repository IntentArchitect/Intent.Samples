using AutoMapper;
using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Angular.Backend.Domain.Entities;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.AutoMapper.DtoMappingProfile", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Customers
{
    public class CustomerLoyaltyDtoProfile : Profile
    {
        public CustomerLoyaltyDtoProfile()
        {
            CreateMap<Loyalty, CustomerLoyaltyDto>();
        }
    }

    public static class CustomerLoyaltyDtoMappingExtensions
    {
        public static CustomerLoyaltyDto MapToCustomerLoyaltyDto(this Loyalty projectFrom, IMapper mapper) => mapper.Map<CustomerLoyaltyDto>(projectFrom);

        public static List<CustomerLoyaltyDto> MapToCustomerLoyaltyDtoList(
            this IEnumerable<Loyalty> projectFrom,
            IMapper mapper) => projectFrom.Select(x => x.MapToCustomerLoyaltyDto(mapper)).ToList();
    }
}