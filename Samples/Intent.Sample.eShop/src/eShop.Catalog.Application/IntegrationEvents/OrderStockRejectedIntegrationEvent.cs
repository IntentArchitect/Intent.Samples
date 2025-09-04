using System.Collections.Generic;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Eventing.Contracts.IntegrationEventMessage", Version = "1.0")]

namespace eShop.Catalog.Eventing.Messages
{
    public record OrderStockRejectedIntegrationEvent
    {
        public OrderStockRejectedIntegrationEvent()
        {
            OrderStockItems = null!;
        }

        public int OrderId { get; init; }
        public List<ConfirmedOrderStockItem> OrderStockItems { get; init; }
    }
}