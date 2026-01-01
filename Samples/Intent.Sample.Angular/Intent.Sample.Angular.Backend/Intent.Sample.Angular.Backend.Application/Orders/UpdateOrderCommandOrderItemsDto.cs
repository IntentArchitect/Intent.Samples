using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Orders
{
    public class UpdateOrderCommandOrderItemsDto
    {
        public UpdateOrderCommandOrderItemsDto()
        {
        }

        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Units { get; set; }
        public decimal Price { get; set; }

        public static UpdateOrderCommandOrderItemsDto Create(Guid id, Guid productId, int units, decimal price)
        {
            return new UpdateOrderCommandOrderItemsDto
            {
                Id = id,
                ProductId = productId,
                Units = units,
                Price = price
            };
        }
    }
}