using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Angular.Backend.Application.Common.Interfaces;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Discounts.UpdateDiscount
{
    public class UpdateDiscountCommand : IRequest, ICommand
    {
        public UpdateDiscountCommand(Guid id, string code, decimal discountAmount, DateTime expiry)
        {
            Id = id;
            Code = code;
            DiscountAmount = discountAmount;
            Expiry = expiry;
        }

        public Guid Id { get; set; }
        public string Code { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime Expiry { get; set; }
    }
}