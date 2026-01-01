using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.QueryValidator", Version = "2.0")]

namespace Intent.Sample.Angular.Backend.Application.Discounts.GetDiscounts
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class GetDiscountsQueryValidator : AbstractValidator<GetDiscountsQuery>
    {
        [IntentManaged(Mode.Merge)]
        public GetDiscountsQueryValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
            // Implement custom validation logic here if required
        }
    }
}