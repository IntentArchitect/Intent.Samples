using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.CommandValidator", Version = "2.0")]

namespace AspNetCoreCleanArchitecture.Sample.Application.Orders.UpdateOrder
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        [IntentManaged(Mode.Merge)]
        public UpdateOrderCommandValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
            RuleFor(v => v.OrderLines)
                .NotNull();
        }
    }
}