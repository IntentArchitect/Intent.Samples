using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.CommandValidator", Version = "2.0")]

namespace Intent.Sample.Angular.Backend.Application.Discounts.UpdateDiscount
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class UpdateDiscountCommandValidator : AbstractValidator<UpdateDiscountCommand>
    {
        [IntentManaged(Mode.Merge)]
        public UpdateDiscountCommandValidator()
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