using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Orders
{
    public class CreateOrderCommandOrderItemsDto
    {
        public CreateOrderCommandOrderItemsDto()
        {
        }

        public Guid ProductId { get; set; }
        public int Units { get; set; }
        public decimal Price { get; set; }

        public static CreateOrderCommandOrderItemsDto Create(Guid productId, int units, decimal price)
        {
            return new CreateOrderCommandOrderItemsDto
            {
                ProductId = productId,
                Units = units,
                Price = price
            };
        }
    }
}