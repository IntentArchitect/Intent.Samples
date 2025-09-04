using System.Collections.Generic;
using eShop.Ordering.Domain.OrderAggregate;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Eventing.Contracts.IntegrationEventMessage", Version = "1.0")]

namespace eShop.Ordering.Eventing.Messages
{
    public record OrderStatusChangedToCancelledIntegrationEvent
    {
        public OrderStatusChangedToCancelledIntegrationEvent()
        {
            BuyerName = null!;
        }
        public int OrderId { get; init; }
        public OrderStatus OrderStatus { get; init; }
        public string BuyerName { get; init; }
    }
}