using System;
using System.Collections.Generic;
using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace eShop.Basket.Domain.Entities
{
    [DefaultIntentManaged(Mode.Fully, Targets = Targets.Methods, Body = Mode.Ignore, AccessModifiers = AccessModifiers.Public)]
    public class CustomerBasket
    {
        public CustomerBasket(string buyerId)
        {
            BuyerId = buyerId;
        }

        [IntentIgnore]
        public CustomerBasket()
        {
            
        }
        public string BuyerId { get; set; }

        public ICollection<BasketItem> BasketItems { get; set; } = [];
    }
}