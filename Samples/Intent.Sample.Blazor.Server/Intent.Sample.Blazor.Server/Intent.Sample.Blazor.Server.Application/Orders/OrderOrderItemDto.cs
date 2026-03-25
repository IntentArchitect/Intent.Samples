using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Application.Orders
{
    public record OrderOrderItemDto
    {
        public OrderOrderItemDto()
        {
            ProductName = null!;
            BrandName = null!;
        }

        public Guid Id { get; init; }
        public Guid ProductId { get; init; }
        public int Units { get; init; }
        public decimal Price { get; init; }
        public string ProductName { get; init; }
        public string BrandName { get; init; }
    }
}