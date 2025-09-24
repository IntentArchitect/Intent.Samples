using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Blazor.FluentValidation.ModelDefinitionValidator", Version = "1.0")]

namespace eShop.Web.Client.Services
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class BasketStateValidator : AbstractValidator<BasketState>
    {
        [IntentManaged(Mode.Merge)]
        public BasketStateValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
            RuleFor(v => v.Id)
                .NotNull();
        }
    }
}