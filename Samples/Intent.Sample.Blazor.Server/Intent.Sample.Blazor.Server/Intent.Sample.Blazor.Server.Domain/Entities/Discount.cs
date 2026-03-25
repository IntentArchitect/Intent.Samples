using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace Intent.Sample.Blazor.Server.Domain.Entities
{
    public class Discount
    {
        public Discount()
        {
            Code = null!;
        }

        public Guid Id { get; set; }

        public string Code { get; set; }

        public decimal DiscountAmount { get; set; }

        public DateTime Expiry { get; set; }
    }
}