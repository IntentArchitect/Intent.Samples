using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Orders
{
    public class OrderOrderItemDto
    {
        public OrderOrderItemDto()
        {
            ProductName = null!;
            BrandName = null!;
        }

        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Units { get; set; }
        public decimal Price { get; set; }
        public string ProductName { get; set; }
        public string BrandName { get; set; }

        public static OrderOrderItemDto Create(
            Guid id,
            Guid productId,
            int units,
            decimal price,
            string productName,
            string brandName)
        {
            return new OrderOrderItemDto
            {
                Id = id,
                ProductId = productId,
                Units = units,
                Price = price,
                ProductName = productName,
                BrandName = brandName
            };
        }
    }
}