using System;
using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.CommandValidator", Version = "2.0")]

namespace eShop.Ordering.Application.Orders.SetStockConfirmedOrderStatus
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class SetStockConfirmedOrderStatusCommandValidator : AbstractValidator<SetStockConfirmedOrderStatusCommand>
    {
        [IntentManaged(Mode.Merge)]
        public SetStockConfirmedOrderStatusCommandValidator()
        {
            ConfigureValidationRules();
        }

        [IntentManaged(Mode.Fully)]
        private void ConfigureValidationRules()
        {
            // Implement custom validation logic here if required
        }
    }
}