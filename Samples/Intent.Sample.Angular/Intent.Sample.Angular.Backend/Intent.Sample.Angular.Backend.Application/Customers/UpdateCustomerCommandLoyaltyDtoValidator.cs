using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.FluentValidation.Dtos.DTOValidator", Version = "2.0")]

namespace Intent.Sample.Angular.Backend.Application.Customers
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class UpdateCustomerCommandLoyaltyDtoValidator : AbstractValidator<UpdateCustomerCommandLoyaltyDto>
    {
        [IntentManaged(Mode.Merge)]
        public UpdateCustomerCommandLoyaltyDtoValidator()
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