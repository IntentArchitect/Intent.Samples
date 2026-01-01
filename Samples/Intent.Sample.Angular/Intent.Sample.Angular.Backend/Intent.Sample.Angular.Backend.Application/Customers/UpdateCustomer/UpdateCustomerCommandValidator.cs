using FluentValidation;
using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Angular.Backend.Application.Common.Validation;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.CommandValidator", Version = "2.0")]

namespace Intent.Sample.Angular.Backend.Application.Customers.UpdateCustomer
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        [IntentManaged(Mode.Merge)]
        public UpdateCustomerCommandValidator(IValidatorProvider provider)
        {
            ConfigureValidationRules(provider);
        }

        private void ConfigureValidationRules(IValidatorProvider provider)
        {
            RuleFor(v => v.Name)
                .NotNull();

            RuleFor(v => v.Surname)
                .NotNull();

            RuleFor(v => v.Email)
                .NotNull();

            RuleFor(v => v.Addresses)
                .NotNull()
                .ForEach(x => x.SetValidator(provider.GetValidator<UpdateCustomerCommandAddressesDto>()!));

            RuleFor(v => v.Loyalty)
                .SetValidator(provider.GetValidator<UpdateCustomerCommandLoyaltyDto>()!);
        }
    }
}