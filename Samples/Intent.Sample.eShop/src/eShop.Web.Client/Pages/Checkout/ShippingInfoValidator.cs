using eShop.Web.Client.Pages.Checkout;
using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Blazor.FluentValidation.ModelDefinitionValidator", Version = "1.0")]

namespace eShop.Web.Client
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class ShippingInfoValidator : AbstractValidator<Checkout.ShippingInfo>
    {
        [IntentManaged(Mode.Merge)]
        public ShippingInfoValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
            RuleFor(v => v.Street)
                .NotNull();

            RuleFor(v => v.City)
                .NotNull();

            RuleFor(v => v.State)
                .NotNull();

            RuleFor(v => v.ZipCode)
                .NotNull();

            RuleFor(v => v.Country)
                .NotNull();
        }
    }
}