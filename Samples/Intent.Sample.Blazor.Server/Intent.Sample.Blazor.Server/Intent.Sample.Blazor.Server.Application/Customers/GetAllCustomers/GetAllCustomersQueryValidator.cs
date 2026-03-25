using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.QueryValidator", Version = "2.0")]

namespace Intent.Sample.Blazor.Server.Application.Customers.GetAllCustomers
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class GetAllCustomersQueryValidator : AbstractValidator<GetAllCustomersQuery>
    {
        [IntentManaged(Mode.Merge)]
        public GetAllCustomersQueryValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
            // Implement custom validation logic here if required
        }
    }
}