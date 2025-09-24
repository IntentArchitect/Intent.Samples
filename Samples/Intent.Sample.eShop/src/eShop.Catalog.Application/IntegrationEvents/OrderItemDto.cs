using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Eventing.Contracts.IntegrationEventDto", Version = "1.0")]

namespace eShop.Ordering.Eventing.Messages
{
    public class OrderItemDto
    {
        public OrderItemDto()
        {
        }

        public int ProductId { get; set; }
        public int Units { get; set; }

        public static OrderItemDto Create(int productId, int units)
        {
            return new OrderItemDto
            {
                ProductId = productId,
                Units = units
            };
        }
    }
}