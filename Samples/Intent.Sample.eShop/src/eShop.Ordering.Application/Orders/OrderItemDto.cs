using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace eShop.Ordering.Application.Orders
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
        public int Units { get; set; }
        public string PictureUrl { get; set; }

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