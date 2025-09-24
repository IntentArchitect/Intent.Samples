using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using eShop.Catalog.Application.Common.Eventing;
using eShop.Catalog.Eventing.Messages;
using eShop.Ordering.Eventing.Messages;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Eventing.MassTransit.IntegrationEventHandler", Version = "1.0")]

namespace eShop.Catalog.Application.IntegrationEvents.EventHandlers
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class OrderStatusChangedToAwaitingValidationIntegrationEventHandler : IIntegrationEventHandler<OrderStatusChangedToAwaitingValidationIntegrationEvent>
    {
        private readonly IEventBus _eventBus;

        [IntentManaged(Mode.Merge)]
        public OrderStatusChangedToAwaitingValidationIntegrationEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task HandleAsync(
            OrderStatusChangedToAwaitingValidationIntegrationEvent message,
            CancellationToken cancellationToken = default)
        {
            _eventBus.Publish(new OrderStockConfirmedIntegrationEvent
            {
                OrderId = message.OrderId
            });
            // IntentIgnore (Match="_eventBus.Publish(new OrderStockRejectedIntegrationEvent")
            _eventBus.Publish(new OrderStockRejectedIntegrationEvent
            {
                OrderId = message.OrderId
            });
        }
    }
}