using eShop.Basket.Application.Common.Validation;
using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.FluentValidation.Dtos.DTOValidator", Version = "2.0")]

namespace eShop.Basket.Application.CustomerBaskets
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class BasketDtoValidator : AbstractValidator<BasketDto>
    {
        [IntentManaged(Mode.Merge)]
        public BasketDtoValidator(IValidatorProvider provider)
        {
            ConfigureValidationRules(provider);
        }

        private void ConfigureValidationRules(IValidatorProvider provider)
        {
            RuleFor(v => v.BuyerId)
                .NotNull();

            RuleFor(v => v.BasketItems)
                .NotNull()
                .ForEach(x => x.SetValidator(provider.GetValidator<BasketItemDto>()!));
        }
    }
}