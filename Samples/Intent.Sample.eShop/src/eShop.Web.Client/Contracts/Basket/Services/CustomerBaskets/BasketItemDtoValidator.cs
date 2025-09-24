using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Blazor.HttpClients.Dtos.FluentValidation.DtoValidator", Version = "2.0")]

namespace eShop.Web.Client.Contracts.Basket.Services.CustomerBaskets
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class BasketItemDtoValidator : AbstractValidator<BasketItemDto>
    {
        [IntentManaged(Mode.Merge)]
        public BasketItemDtoValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
            RuleFor(v => v.Id)
                .NotNull();

            RuleFor(v => v.ProductName)
                .NotNull();

            RuleFor(v => v.PictureUrl)
                .NotNull();
        }
    }
}