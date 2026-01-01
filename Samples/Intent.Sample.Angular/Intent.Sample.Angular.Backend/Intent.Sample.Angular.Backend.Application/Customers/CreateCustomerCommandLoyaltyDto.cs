using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Customers
{
    public class CreateCustomerCommandLoyaltyDto
    {
        public CreateCustomerCommandLoyaltyDto()
        {
            LoyaltyNo = null!;
        }

        public string LoyaltyNo { get; set; }
        public int Points { get; set; }

        public static CreateCustomerCommandLoyaltyDto Create(string loyaltyNo, int points)
        {
            return new CreateCustomerCommandLoyaltyDto
            {
                LoyaltyNo = loyaltyNo,
                Points = points
            };
        }
    }
}