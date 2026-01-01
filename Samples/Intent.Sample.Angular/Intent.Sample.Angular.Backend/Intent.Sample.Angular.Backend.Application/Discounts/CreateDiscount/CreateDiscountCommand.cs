using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Angular.Backend.Application.Common.Interfaces;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Discounts.CreateDiscount
{
    public class CreateDiscountCommand : IRequest<Guid>, ICommand
    {
        public CreateDiscountCommand(string code, decimal discountAmount, DateTime expiry)
        {
            Code = code;
            DiscountAmount = discountAmount;
            Expiry = expiry;
        }

        public string Code { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime Expiry { get; set; }
    }
}