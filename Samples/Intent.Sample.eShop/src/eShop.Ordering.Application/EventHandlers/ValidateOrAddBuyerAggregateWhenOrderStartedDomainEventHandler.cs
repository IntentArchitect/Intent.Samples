using System;
using System.Threading;
using System.Threading.Tasks;
using eShop.Ordering.Application.Common.Eventing;
using eShop.Ordering.Application.Common.Models;
using eShop.Ordering.Domain.BuyerAggregate;
using eShop.Ordering.Domain.Events;
using eShop.Ordering.Domain.Repositories.BuyerAggregate;
using eShop.Ordering.Eventing.Messages;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.MediatR.DomainEvents.DomainEventHandler", Version = "1.0")]

namespace eShop.Ordering.Application.EventHandlers
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class ValidateOrAddBuyerAggregateWhenOrderStartedDomainEventHandler : INotificationHandler<DomainEventNotification<OrderStartedDomainEvent>>
    {
        private readonly IBuyerRepository _buyerRepository;
        private readonly IEventBus _eventBus;

        [IntentManaged(Mode.Merge)]
        public ValidateOrAddBuyerAggregateWhenOrderStartedDomainEventHandler(IBuyerRepository buyerRepository,
            IEventBus eventBus)
        {
            _buyerRepository = buyerRepository;
            _eventBus = eventBus;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Merge)]
        public async Task Handle(
            DomainEventNotification<OrderStartedDomainEvent> notification,
            CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            var buyer = await _buyerRepository.FindAsync(x => x.BuyerIdentifier == domainEvent.UserId, cancellationToken);

            if (buyer == null)
            {
                buyer = new Buyer(domainEvent.UserId, domainEvent.UserName);
                _buyerRepository.Add(buyer);
                await _buyerRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            }
            // IntentIgnore
            _eventBus.Publish(new OrderStatusChangedToSubmittedIntegrationEvent
            {
                OrderId = domainEvent.Order.Id,
                OrderStatus = Domain.OrderAggregate.OrderStatus.Submitted,
                BuyerName = domainEvent.Order.Buyer?.Name
            });

        }
    }
}