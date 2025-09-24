using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Eventing.Contracts.IntegrationEventMessage", Version = "1.0")]

namespace eShop.Catalog.Eventing.Messages
{
    public record OrderStockConfirmedIntegrationEvent
    {
        public int OrderId { get; init; }
    }
}