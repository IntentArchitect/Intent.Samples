using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Blazor.HttpClients.Dtos.FluentValidation.DtoValidator", Version = "2.0")]

namespace eShop.Web.Client.Contracts.Catalog.Services.CatalogItems
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class CatalogBrandDtoValidator : AbstractValidator<CatalogBrandDto>
    {
        [IntentManaged(Mode.Merge)]
        public CatalogBrandDtoValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
            RuleFor(v => v.Brand)
                .NotNull();
        }
    }
}