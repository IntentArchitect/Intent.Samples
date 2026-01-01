using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Customers
{
    public class CustomerLoyaltyDto
    {
        public CustomerLoyaltyDto()
        {
            LoyaltyNo = null!;
        }

        public Guid Id { get; set; }
        public string LoyaltyNo { get; set; }
        public int Points { get; set; }

        public static CustomerLoyaltyDto Create(Guid id, string loyaltyNo, int points)
        {
            return new CustomerLoyaltyDto
            {
                Id = id,
                LoyaltyNo = loyaltyNo,
                Points = points
            };
        }
    }
}