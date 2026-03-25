using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.QueryValidator", Version = "2.0")]

namespace Intent.Sample.Blazor.Server.Application.Discounts.GetDiscountById
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class GetDiscountByIdQueryValidator : AbstractValidator<GetDiscountByIdQuery>
    {
        [IntentManaged(Mode.Merge)]
        public GetDiscountByIdQueryValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
            // Implement custom validation logic here if required
        }
    }
}