using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Blazor.HttpClients.Dtos.FluentValidation.DtoValidator", Version = "2.0")]

namespace eShop.Web.Client.Contracts.Ordering.Services.Orders
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
    {
        [IntentManaged(Mode.Merge)]
        public OrderItemDtoValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
            RuleFor(v => v.ProductName)
                .NotNull();

            RuleFor(v => v.PictureUrl)
                .NotNull();
        }
    }
}