using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Eventing.Contracts.IntegrationEventMessage", Version = "1.0")]

namespace eShop.Catalog.Eventing.Messages
{
    public record ProductPriceChangedIntegrationEvent
    {
        public int ProductId { get; init; }
        public decimal NewPrice { get; init; }
        public decimal OldPrice { get; init; }
    }
}