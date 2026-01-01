using AutoMapper;
using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Angular.Backend.Domain.Entities;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.AutoMapper.DtoMappingProfile", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Orders
{
    public class OrderDtoProfile : Profile
    {
        public OrderDtoProfile()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(d => d.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
                .ForMember(d => d.CustomerSurname, opt => opt.MapFrom(src => src.Customer.Surname))
                .ForMember(d => d.CustomerEmail, opt => opt.MapFrom(src => src.Customer.Email))
                .ForMember(d => d.TitleName, opt => opt.MapFrom(src => src.Customer.Title.Name))
                .ForMember(d => d.OrderItems, opt => opt.MapFrom(src => src.OrderItems));
        }
    }

    public static class OrderDtoMappingExtensions
    {
        public static OrderDto MapToOrderDto(this Order projectFrom, IMapper mapper) => mapper.Map<OrderDto>(projectFrom);

        public static List<OrderDto> MapToOrderDtoList(this IEnumerable<Order> projectFrom, IMapper mapper) => projectFrom.Select(x => x.MapToOrderDto(mapper)).ToList();
    }
}