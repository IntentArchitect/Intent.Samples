using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.QueryValidator", Version = "2.0")]

namespace MudBlazor.Sample.Application.Products.GetProductById
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
    {
        [IntentManaged(Mode.Merge)]
        public GetProductByIdQueryValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
            // Implement custom validation logic here if required
        }
    }
}