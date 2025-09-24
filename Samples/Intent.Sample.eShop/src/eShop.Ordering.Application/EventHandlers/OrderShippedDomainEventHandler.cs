using System;
using System.Threading;
using System.Threading.Tasks;
using eShop.Ordering.Application.Common.Eventing;
using eShop.Ordering.Application.Common.Models;
using eShop.Ordering.Domain.Events;
using eShop.Ordering.Eventing.Messages;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.MediatR.DomainEvents.DomainEventHandler", Version = "1.0")]

namespace eShop.Ordering.Application.EventHandlers
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class OrderShippedDomainEventHandler : INotificationHandler<DomainEventNotification<OrderShippedDomainEvent>>
    {
        private readonly IEventBus _eventBus;
        public OrderShippedDomainEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task Handle(
            DomainEventNotification<OrderShippedDomainEvent> notification,
            CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;
            _eventBus.Publish(new OrderStatusChangedToShippedIntegrationEvent
            {
                OrderId = domainEvent.Order.Id,
                OrderStatus = domainEvent.Order.OrderStatus,
                BuyerName = domainEvent.Order.Buyer.Name
            });
        }
    }
}