using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eShop.Ordering.Domain.Repositories.OrderAggregate;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryHandler", Version = "1.0")]

namespace eShop.Ordering.Application.Orders.GetOrders
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, List<OrderSummaryDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        [IntentManaged(Mode.Ignore)]
        public GetOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<List<OrderSummaryDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.FindAllAsync(cancellationToken);
            return orders.MapToOrderSummaryDtoList(_mapper);
        }
    }
}