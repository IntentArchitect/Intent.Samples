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
    public static class OrderSummaryDtoMappingExtensions
    {
        public static OrderSummaryDto MapToOrderSummaryDto(this Order projectFrom, IMapper mapper)
            => mapper.Map<OrderSummaryDto>(projectFrom);

        public static List<OrderSummaryDto> MapToOrderSummaryDtoList(this IEnumerable<Order> projectFrom, IMapper mapper)
            => projectFrom.Select(x => x.MapToOrderSummaryDto(mapper)).ToList();
    }
}