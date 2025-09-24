using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Blazor.FluentValidation.ModelDefinitionValidator", Version = "1.0")]

namespace MudBlazor.Sample.Client.Pages.Products
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class MyNewModelDefinitionValidator : AbstractValidator<NewComponent.MyNewModelDefinition>
    {
        [IntentManaged(Mode.Merge)]
        public MyNewModelDefinitionValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
            RuleFor(v => v.Field1)
                .NotNull();
        }
    }
}