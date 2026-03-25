using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace Intent.Sample.Blazor.Server.Domain.Entities
{
    public class Loyalty
    {
        public Loyalty()
        {
            LoyaltyNo = null!;
        }

        public Guid Id { get; set; }

        public string LoyaltyNo { get; set; }

        public int Points { get; set; }
    }
}