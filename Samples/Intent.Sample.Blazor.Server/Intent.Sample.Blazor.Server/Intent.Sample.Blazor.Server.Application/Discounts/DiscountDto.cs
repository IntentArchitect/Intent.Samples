using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Application.Discounts
{
    public record DiscountDto
    {
        public DiscountDto()
        {
            Code = null!;
        }

        public Guid Id { get; init; }
        public string Code { get; init; }
        public decimal DiscountAmount { get; init; }
        public DateTime Expiry { get; init; }
    }
}