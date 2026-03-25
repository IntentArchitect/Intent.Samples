using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.QueryValidator", Version = "2.0")]

namespace Intent.Sample.Blazor.Server.Application.Titles.GetTitles
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class GetTitlesQueryValidator : AbstractValidator<GetTitlesQuery>
    {
        [IntentManaged(Mode.Merge)]
        public GetTitlesQueryValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
            // Implement custom validation logic here if required
        }
    }
}