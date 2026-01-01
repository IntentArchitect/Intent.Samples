using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.QueryValidator", Version = "2.0")]

namespace Intent.Sample.Angular.Backend.Application.Titles.GetTitleById
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class GetTitleByIdQueryValidator : AbstractValidator<GetTitleByIdQuery>
    {
        [IntentManaged(Mode.Merge)]
        public GetTitleByIdQueryValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
            // Implement custom validation logic here if required
        }
    }
}