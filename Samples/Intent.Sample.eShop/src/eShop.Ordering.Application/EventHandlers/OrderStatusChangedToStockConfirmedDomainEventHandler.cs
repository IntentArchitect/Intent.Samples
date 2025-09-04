using System;
using System.Threading;
using System.Threading.Tasks;
using eShop.Ordering.Application.Common.Eventing;
using eShop.Ordering.Application.Common.Models;
using eShop.Ordering.Domain.Events;
using eShop.Ordering.Eventing.Messages;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.MediatR.DomainEvents.DomainEventHandler", Version = "1.0")]

namespace eShop.Ordering.Application.EventHandlers
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class OrderStatusChangedToStockConfirmedDomainEventHandler : INotificationHandler<DomainEventNotification<OrderStatusChangedToStockConfirmedDomainEvent>>
    {
        private readonly IEventBus _eventBus;

        [IntentManaged(Mode.Merge)]
        public OrderStatusChangedToStockConfirmedDomainEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task Handle(
            DomainEventNotification<OrderStatusChangedToStockConfirmedDomainEvent> notification,
            CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;
            _eventBus.Publish(new OrderStatusChangedToStockConfirmedIntegrationEvent
            {
                OrderId = domainEvent.Order.Id,
                OrderStatus = domainEvent.Order.OrderStatus,
                BuyerName = domainEvent.Order.Buyer.Name
            });
        }
    }
}