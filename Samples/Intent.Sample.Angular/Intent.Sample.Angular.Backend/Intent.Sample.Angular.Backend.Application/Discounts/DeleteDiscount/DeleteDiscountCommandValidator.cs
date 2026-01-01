using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.CommandValidator", Version = "2.0")]

namespace Intent.Sample.Angular.Backend.Application.Discounts.DeleteDiscount
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class DeleteDiscountCommandValidator : AbstractValidator<DeleteDiscountCommand>
    {
        [IntentManaged(Mode.Merge)]
        public DeleteDiscountCommandValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
            // Implement custom validation logic here if required
        }
    }
}