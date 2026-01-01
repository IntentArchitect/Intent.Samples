using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.CommandValidator", Version = "2.0")]

namespace Intent.Sample.Angular.Backend.Application.Discounts.CreateDiscount
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class CreateDiscountCommandValidator : AbstractValidator<CreateDiscountCommand>
    {
        [IntentManaged(Mode.Merge)]
        public CreateDiscountCommandValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
            RuleFor(v => v.Code)
                .NotNull();
        }
    }
}