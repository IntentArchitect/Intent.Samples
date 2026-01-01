using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.CommandValidator", Version = "2.0")]

namespace Intent.Sample.Angular.Backend.Application.Titles.UpdateTitle
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class UpdateTitleCommandValidator : AbstractValidator<UpdateTitleCommand>
    {
        [IntentManaged(Mode.Merge)]
        public UpdateTitleCommandValidator()
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