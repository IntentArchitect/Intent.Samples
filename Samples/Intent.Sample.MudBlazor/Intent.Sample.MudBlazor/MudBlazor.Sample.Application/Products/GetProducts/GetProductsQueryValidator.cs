using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.QueryValidator", Version = "2.0")]

namespace MudBlazor.Sample.Application.Products.GetProducts
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class GetProductsQueryValidator : AbstractValidator<GetProductsQuery>
    {
        [IntentManaged(Mode.Merge)]
        public GetProductsQueryValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
            // Implement custom validation logic here if required
        }
    }
}