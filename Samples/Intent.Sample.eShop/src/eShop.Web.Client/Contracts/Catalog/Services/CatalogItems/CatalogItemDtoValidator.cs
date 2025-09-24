using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Blazor.HttpClients.Dtos.FluentValidation.DtoValidator", Version = "2.0")]

namespace eShop.Web.Client.Contracts.Catalog.Services.CatalogItems
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class CatalogItemDtoValidator : AbstractValidator<CatalogItemDto>
    {
        [IntentManaged(Mode.Merge)]
        public CatalogItemDtoValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
            RuleFor(v => v.Name)
                .NotNull();

            RuleFor(v => v.Description)
                .NotNull();

            RuleFor(v => v.PictureFileName)
                .NotNull();

            RuleFor(v => v.PictureUri)
                .NotNull();
        }
    }
}