using AutoMapper;
using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Angular.Backend.Domain.Entities;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.AutoMapper.DtoMappingProfile", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Orders
{
    public class OrderSummaryDtoProfile : Profile
    {
        public OrderSummaryDtoProfile()
        {
            CreateMap<Order, OrderSummaryDto>()
                .ForMember(d => d.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
                .ForMember(d => d.CustomerSurname, opt => opt.MapFrom(src => src.Customer.Surname))
                .ForMember(d => d.CustomerEmail, opt => opt.MapFrom(src => src.Customer.Email))
                .ForMember(d => d.TitleName, opt => opt.MapFrom(src => src.Customer.Title.Name));
        }
    }

    public static class OrderSummaryDtoMappingExtensions
    {
        public static OrderSummaryDto MapToOrderSummaryDto(this Order projectFrom, IMapper mapper) => mapper.Map<OrderSummaryDto>(projectFrom);

        public static List<OrderSummaryDto> MapToOrderSummaryDtoList(this IEnumerable<Order> projectFrom, IMapper mapper) => projectFrom.Select(x => x.MapToOrderSummaryDto(mapper)).ToList();
    }
}