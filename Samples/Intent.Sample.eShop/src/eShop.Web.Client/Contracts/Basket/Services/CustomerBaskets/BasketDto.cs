using System.Collections.Generic;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.Blazor.HttpClients.DtoContract", Version = "2.0")]

namespace eShop.Web.Client.Contracts.Basket.Services.CustomerBaskets
{
    public class BasketDto
    {
        public BasketDto()
        {
            BuyerId = null!;
            BasketItems = [];
        }

        public string BuyerId { get; set; }
        public List<BasketItemDto> BasketItems { get; set; }

        public static BasketDto Create(string buyerId, List<BasketItemDto> basketItems)
        {
            return new BasketDto
            {
                BuyerId = buyerId,
                BasketItems = basketItems
            };
        }
    }
}