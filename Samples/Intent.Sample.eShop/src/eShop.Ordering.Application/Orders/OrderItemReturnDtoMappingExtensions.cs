using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using AutoMapper;
using eShop.Ordering.Domain.OrderAggregate;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.AutoMapper.MappingExtensions", Version = "1.0")]

namespace eShop.Ordering.Application.Orders
{
    public static class OrderItemReturnDtoMappingExtensions
    {
        public static OrderItemReturnDto MapToOrderItemReturnDto(this OrderItem projectFrom, IMapper mapper)
            => mapper.Map<OrderItemReturnDto>(projectFrom);

        public static List<OrderItemReturnDto> MapToOrderItemReturnDtoList(this IEnumerable<OrderItem> projectFrom, IMapper mapper)
            => projectFrom.Select(x => x.MapToOrderItemReturnDto(mapper)).ToList();
    }
}