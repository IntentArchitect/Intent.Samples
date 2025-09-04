using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using eShop.Ordering.Domain.Common.Exceptions;
using eShop.Ordering.Domain.Repositories.OrderAggregate;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandHandler", Version = "2.0")]

namespace eShop.Ordering.Application.Orders.SetStockRejectedOrderStatus
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class SetStockRejectedOrderStatusCommandHandler : IRequestHandler<SetStockRejectedOrderStatusCommand>
    {
        private readonly IOrderRepository _orderRepository;

        [IntentManaged(Mode.Merge)]
        public SetStockRejectedOrderStatusCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task Handle(SetStockRejectedOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.FindByIdAsync(request.OrderId, cancellationToken);
            if (order is null)
            {
                throw new NotFoundException($"Could not find Order '{request.OrderId}'");
            }

            order.SetCancelledStatusWhenStockIsRejected(request.OrderStockItems);

        }
    }
}