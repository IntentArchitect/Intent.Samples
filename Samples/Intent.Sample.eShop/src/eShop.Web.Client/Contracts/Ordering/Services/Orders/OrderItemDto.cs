using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.Blazor.HttpClients.DtoContract", Version = "2.0")]

namespace eShop.Web.Client.Contracts.Ordering.Services.Orders
{
    public class OrderItemDto
    {
        public OrderItemDto()
        {
            ProductName = null!;
            PictureUrl = null!;
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public string PictureUrl { get; set; }
        public int Units { get; set; }

        public static OrderItemDto Create(
            int productId,
            string productName,
            decimal unitPrice,
            decimal discount,
            string pictureUrl,
            int units)
        {
            return new OrderItemDto
            {
                ProductId = productId,
                ProductName = productName,
                UnitPrice = unitPrice,
                Discount = discount,
                PictureUrl = pictureUrl,
                Units = units
            };
        }
    }
}