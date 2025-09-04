using System;
using System.Threading;
using System.Threading.Tasks;
using eShop.Catalog.Eventing.Messages;
using eShop.Ordering.Application.Common.Eventing;
using eShop.Ordering.Application.Orders.SetStockConfirmedOrderStatus;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Eventing.MassTransit.IntegrationEventHandler", Version = "1.0")]

namespace eShop.Ordering.Application.IntegrationEvents.EventHandlers
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class OrderStockConfirmedIntegrationEventHandler : IIntegrationEventHandler<OrderStockConfirmedIntegrationEvent>
    {
        private readonly ISender _mediator;

        [IntentManaged(Mode.Merge)]
        public OrderStockConfirmedIntegrationEventHandler(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task HandleAsync(
            OrderStockConfirmedIntegrationEvent message,
            CancellationToken cancellationToken = default)
        {
            var command = new SetStockConfirmedOrderStatusCommand(orderId: message.OrderId);

            await _mediator.Send(command, cancellationToken);
        }
    }
}