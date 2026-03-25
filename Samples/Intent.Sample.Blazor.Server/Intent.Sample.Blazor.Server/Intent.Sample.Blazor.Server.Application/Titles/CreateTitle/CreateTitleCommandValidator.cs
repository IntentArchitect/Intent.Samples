using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.CommandValidator", Version = "2.0")]

namespace Intent.Sample.Blazor.Server.Application.Titles.CreateTitle
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class CreateTitleCommandValidator : AbstractValidator<CreateTitleCommand>
    {
        [IntentManaged(Mode.Merge)]
        public CreateTitleCommandValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
            RuleFor(v => v.Name)
                .NotNull();
        }
    }
}