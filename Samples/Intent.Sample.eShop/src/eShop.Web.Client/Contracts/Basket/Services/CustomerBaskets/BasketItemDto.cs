using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.Blazor.HttpClients.DtoContract", Version = "2.0")]

namespace eShop.Web.Client.Contracts.Basket.Services.CustomerBaskets
{
    public class BasketItemDto
    {
        public BasketItemDto()
        {
            Id = null!;
            ProductName = null!;
            PictureUrl = null!;
        }

        public string Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal OldUnitPrice { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; }

        public static BasketItemDto Create(
            string id,
            int productId,
            string productName,
            decimal unitPrice,
            decimal oldUnitPrice,
            int quantity,
            string pictureUrl)
        {
            return new BasketItemDto
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