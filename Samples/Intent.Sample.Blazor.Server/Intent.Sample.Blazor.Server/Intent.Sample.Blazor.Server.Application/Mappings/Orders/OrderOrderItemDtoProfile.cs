using AutoMapper;
using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Domain.Entities;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.AutoMapper.DtoMappingProfile", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Application.Orders
{
    public class OrderOrderItemDtoProfile : Profile
    {
        public OrderOrderItemDtoProfile()
        {
            CreateMap<OrderItem, OrderOrderItemDto>()
                .ForMember(d => d.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(d => d.BrandName, opt => opt.MapFrom(src => src.Product.Brand.Name));
        }
    }

    public static class OrderOrderItemDtoMappingExtensions
    {
        public static OrderOrderItemDto MapToOrderOrderItemDto(this OrderItem projectFrom, IMapper mapper) => mapper.Map<OrderOrderItemDto>(projectFrom);

        public static List<OrderOrderItemDto> MapToOrderOrderItemDtoList(
            this IEnumerable<OrderItem> projectFrom,
            IMapper mapper) => projectFrom.Select(x => x.MapToOrderOrderItemDto(mapper)).ToList();
    }
}