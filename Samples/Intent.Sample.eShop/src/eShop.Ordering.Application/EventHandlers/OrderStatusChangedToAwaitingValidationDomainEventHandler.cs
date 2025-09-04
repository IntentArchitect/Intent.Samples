using System;
using System.Linq;
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
    public class OrderStatusChangedToAwaitingValidationDomainEventHandler : INotificationHandler<DomainEventNotification<OrderStatusChangedToAwaitingValidationDomainEvent>>
    {
        private readonly IEventBus _eventBus;

        [IntentManaged(Mode.Merge)]
        public OrderStatusChangedToAwaitingValidationDomainEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task Handle(
            DomainEventNotification<OrderStatusChangedToAwaitingValidationDomainEvent> notification,
            CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;
            _eventBus.Publish(new OrderStatusChangedToAwaitingValidationIntegrationEvent
            {
                OrderId = domainEvent.Order.Id,
                BuyerName = domainEvent.Order.Buyer.Name,
                OrderItems = domainEvent.Order.OrderItems
                    .Select(oi => new OrderItemDto
                    {
                        ProductId = oi.ProductId,
                        Units = oi.Units
                    })
                    .ToList()
            });
        }
    }
}