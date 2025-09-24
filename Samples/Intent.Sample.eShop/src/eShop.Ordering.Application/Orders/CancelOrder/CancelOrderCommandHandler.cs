using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using eShop.Ordering.Application.Common.Eventing;
using eShop.Ordering.Domain.Common.Exceptions;
using eShop.Ordering.Domain.Repositories.OrderAggregate;
using eShop.Ordering.Eventing.Messages;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandHandler", Version = "2.0")]

namespace eShop.Ordering.Application.Orders.CancelOrder
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEventBus _eventBus;

        [IntentManaged(Mode.Merge)]
        public CancelOrderCommandHandler(IOrderRepository orderRepository, IEventBus eventBus)
        {
            _orderRepository = orderRepository;
            _eventBus = eventBus;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.FindByIdAsync(request.OrderId, cancellationToken);
            if (order is null)
            {
                throw new NotFoundException($"Could not find Order '{request.OrderId}'");
            }

            order.SetCancelledStatus();

        }
    }
}