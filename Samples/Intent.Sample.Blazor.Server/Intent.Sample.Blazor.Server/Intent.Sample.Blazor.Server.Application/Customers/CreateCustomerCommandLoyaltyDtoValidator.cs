using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.FluentValidation.Dtos.DTOValidator", Version = "2.0")]

namespace Intent.Sample.Blazor.Server.Application.Customers
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class CreateCustomerCommandLoyaltyDtoValidator : AbstractValidator<CreateCustomerCommandLoyaltyDto>
    {
        [IntentManaged(Mode.Merge)]
        public CreateCustomerCommandLoyaltyDtoValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
            RuleFor(v => v.LoyaltyNo)
                .NotNull();
        }
    }
}