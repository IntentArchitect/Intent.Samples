using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Discounts
{
    public class DiscountDto
    {
        public DiscountDto()
        {
            Code = null!;
        }

        public Guid Id { get; set; }
        public string Code { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime Expiry { get; set; }

        public static DiscountDto Create(Guid id, string code, decimal discountAmount, DateTime expiry)
        {
            return new DiscountDto
            {
                Id = id,
                Code = code,
                DiscountAmount = discountAmount,
                Expiry = expiry
            };
        }
    }
}