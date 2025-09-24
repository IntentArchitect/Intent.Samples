using System;
using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.FluentValidation.CommandValidator", Version = "2.0")]

namespace eShop.Ordering.Application.Orders.SetStockRejectedOrderStatus
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class SetStockRejectedOrderStatusCommandValidator : AbstractValidator<SetStockRejectedOrderStatusCommand>
    {
        [IntentManaged(Mode.Merge)]
        public SetStockRejectedOrderStatusCommandValidator()
        {
            ConfigureValidationRules();
        }

        [IntentManaged(Mode.Fully)]
        private void ConfigureValidationRules()
        {
            RuleFor(v => v.OrderStockItems)
                .NotNull();
        }
    }
}