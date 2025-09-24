using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Eventing.Contracts.IntegrationEventMessage", Version = "1.0")]

namespace eShop.Ordering.Eventing.Messages
{
    public record OrderStartedIntegrationEvent
    {
        public OrderStartedIntegrationEvent()
        {
            UserId = null!;
        }
        public string UserId { get; init; }
    }
}