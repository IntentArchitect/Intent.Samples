using System.Collections.Generic;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Eventing.Contracts.IntegrationEventMessage", Version = "1.0")]

namespace eShop.Ordering.Eventing.Messages
{
    public record OrderStatusChangedToAwaitingValidationIntegrationEvent
    {
        public OrderStatusChangedToAwaitingValidationIntegrationEvent()
        {
            OrderStatus = null!;
            BuyerName = null!;
            OrderItems = null!;
        }
        public int OrderId { get; init; }
        public string OrderStatus { get; init; }
        public string BuyerName { get; init; }
        public List<OrderItemDto> OrderItems { get; init; }
    }
}