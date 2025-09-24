using System;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Eventing.Contracts.IntegrationEventMessage", Version = "1.0")]

namespace eShop.Basket.Eventing.Messages
{
    public record UserCheckoutAcceptedIntegrationEvent
    {
        public UserCheckoutAcceptedIntegrationEvent()
        {
            UserId = null!;
            UserName = null!;
            City = null!;
            Street = null!;
            State = null!;
            Country = null!;
            ZipCode = null!;
            CardNumber = null!;
            CardHolderName = null!;
            CardSecurityNumber = null!;
            Basket = null!;
        }
        public string UserId { get; init; }
        public string UserName { get; init; }
        public int OrderNumber { get; init; }
        public string City { get; init; }
        public string Street { get; init; }
        public string State { get; init; }
        public string Country { get; init; }
        public string ZipCode { get; init; }
        public string CardNumber { get; init; }
        public string CardHolderName { get; init; }
        public DateTime CardExpiration { get; init; }
        public string CardSecurityNumber { get; init; }
        public int CardTypeId { get; init; }
        public int Buyer { get; init; }
        public Guid RequestId { get; init; }
        public CustomerBasket Basket { get; init; }
    }
}