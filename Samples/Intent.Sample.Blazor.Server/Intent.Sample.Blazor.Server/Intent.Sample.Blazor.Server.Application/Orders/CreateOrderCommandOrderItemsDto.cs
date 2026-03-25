using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Application.Orders
{
    public record CreateOrderCommandOrderItemsDto
    {
        public CreateOrderCommandOrderItemsDto()
        {
        }

        public Guid ProductId { get; init; }
        public int Units { get; init; }
        public decimal Price { get; init; }
    }
}