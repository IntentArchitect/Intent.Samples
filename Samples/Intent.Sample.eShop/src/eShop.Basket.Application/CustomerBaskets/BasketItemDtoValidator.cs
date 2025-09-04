using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.FluentValidation.Dtos.DTOValidator", Version = "2.0")]

namespace eShop.Basket.Application.CustomerBaskets
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class BasketItemDtoValidator : AbstractValidator<BasketItemDto>
    {
        [IntentManaged(Mode.Merge)]
        public BasketItemDtoValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
            RuleFor(v => v.Id)
                .NotNull();

            RuleFor(v => v.ProductName)
                .NotNull();

            RuleFor(v => v.PictureUrl)
                .NotNull();
        }
    }
}