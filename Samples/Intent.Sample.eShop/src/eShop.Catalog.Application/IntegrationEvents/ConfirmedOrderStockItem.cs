using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Eventing.Contracts.IntegrationEventDto", Version = "1.0")]

namespace eShop.Catalog.Eventing.Messages
{
    public class ConfirmedOrderStockItem
    {
        public ConfirmedOrderStockItem()
        {
        }

        public int ProductId { get; set; }
        public bool HasStock { get; set; }

        public static ConfirmedOrderStockItem Create(int productId, bool hasStock)
        {
            return new ConfirmedOrderStockItem
            {
                ProductId = productId,
                HasStock = hasStock
            };
        }
    }
}