using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.FluentValidation.Dtos.DTOValidator", Version = "2.0")]

namespace eShop.Catalog.Application.CatalogItems
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class CatalogItemCreateDtoValidator : AbstractValidator<CatalogItemCreateDto>
    {
        [IntentManaged(Mode.Merge)]
        public CatalogItemCreateDtoValidator()
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