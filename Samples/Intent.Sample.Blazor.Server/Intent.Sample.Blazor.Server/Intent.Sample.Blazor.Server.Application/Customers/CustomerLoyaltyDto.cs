using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Application.Customers
{
    public record CustomerLoyaltyDto
    {
        public CustomerLoyaltyDto()
        {
            LoyaltyNo = null!;
        }

        public Guid Id { get; init; }
        public string LoyaltyNo { get; init; }
        public int Points { get; init; }
    }
}