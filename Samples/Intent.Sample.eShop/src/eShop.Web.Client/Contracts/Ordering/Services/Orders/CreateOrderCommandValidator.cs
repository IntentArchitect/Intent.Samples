using eShop.Web.Client.Common.Validation;
using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Blazor.HttpClients.Dtos.FluentValidation.DtoValidator", Version = "2.0")]

namespace eShop.Web.Client.Contracts.Ordering.Services.Orders
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        [IntentManaged(Mode.Merge)]
        public CreateOrderCommandValidator(IValidatorProvider provider)
        {
            ConfigureValidationRules(provider);
        }

        private void ConfigureValidationRules(IValidatorProvider provider)
        {
            RuleFor(v => v.UserId)
                .NotNull();

            RuleFor(v => v.UserName)
                .NotNull();

            RuleFor(v => v.Street)
                .NotNull();

            RuleFor(v => v.City)
                .NotNull();

            RuleFor(v => v.State)
                .NotNull();

            RuleFor(v => v.Country)
                .NotNull();

            RuleFor(v => v.ZipCode)
                .NotNull();

            RuleFor(v => v.OrderItems)
                .NotNull()
                .ForEach(x => x.SetValidator(provider.GetValidator<OrderItemDto>()!));
        }
    }
}