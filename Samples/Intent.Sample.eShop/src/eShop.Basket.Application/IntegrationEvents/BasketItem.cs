using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Eventing.Contracts.IntegrationEventDto", Version = "1.0")]

namespace eShop.Basket.Eventing.Messages
{
    public class BasketItem
    {
        public BasketItem()
        {
        }

        public string Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal OldUnitPrice { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; }

        public static BasketItem Create(
            string id,
            int productId,
            string productName,
            decimal unitPrice,
            decimal oldUnitPrice,
            int quantity,
            string pictureUrl)
        {
            return new BasketItem
            {
                Id = id,
                ProductId = productId,
                ProductName = productName,
                UnitPrice = unitPrice,
                OldUnitPrice = oldUnitPrice,
                Quantity = quantity,
                PictureUrl = pictureUrl
            };
        }
    }
}