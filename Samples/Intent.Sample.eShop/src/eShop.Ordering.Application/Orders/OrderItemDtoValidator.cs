using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.FluentValidation.Dtos.DTOValidator", Version = "2.0")]

namespace eShop.Ordering.Application.Orders
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
    {
        [IntentManaged(Mode.Merge)]
        public OrderItemDtoValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
            RuleFor(v => v.ProductName)
                .NotNull();

            RuleFor(v => v.PictureUrl)
                .NotNull();
        }
    }
}