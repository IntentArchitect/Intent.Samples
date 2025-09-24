using System;
using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.CommandValidator", Version = "2.0")]

namespace eShop.Ordering.Application.Orders.CancelOrder
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
    {
        [IntentManaged(Mode.Merge)]
        public CancelOrderCommandValidator()
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