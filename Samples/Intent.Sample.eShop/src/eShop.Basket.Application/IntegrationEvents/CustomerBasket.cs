using System.Collections.Generic;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Eventing.Contracts.IntegrationEventDto", Version = "1.0")]

namespace eShop.Basket.Eventing.Messages
{
    public class CustomerBasket
    {
        public CustomerBasket()
        {
        }

        public int BuyerId { get; set; }
        public List<BasketItem> Items { get; set; }

        public static CustomerBasket Create(int buyerId, List<BasketItem> items)
        {
            return new CustomerBasket
            {
                BuyerId = buyerId,
                Items = items
            };
        }
    }
}