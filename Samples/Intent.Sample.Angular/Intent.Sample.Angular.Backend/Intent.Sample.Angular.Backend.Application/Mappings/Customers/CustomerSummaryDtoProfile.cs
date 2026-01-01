using AutoMapper;
using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Angular.Backend.Domain.Entities;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.AutoMapper.DtoMappingProfile", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Customers
{
    public class CustomerSummaryDtoProfile : Profile
    {
        public CustomerSummaryDtoProfile()
        {
            CreateMap<Customer, CustomerSummaryDto>();
        }
    }

    public static class CustomerSummaryDtoMappingExtensions
    {
        public static CustomerSummaryDto MapToCustomerSummaryDto(this Customer projectFrom, IMapper mapper) => mapper.Map<CustomerSummaryDto>(projectFrom);

        public static List<CustomerSummaryDto> MapToCustomerSummaryDtoList(
            this IEnumerable<Customer> projectFrom,
            IMapper mapper) => projectFrom.Select(x => x.MapToCustomerSummaryDto(mapper)).ToList();
    }
}