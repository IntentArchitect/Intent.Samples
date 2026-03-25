using System.ComponentModel.DataAnnotations;
using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Application.Brands;
using Intent.Sample.Blazor.Server.Application.Brands.GetBrands;
using Intent.Sample.Blazor.Server.Application.Products.CreateProduct;
using Intent.Sample.Blazor.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.Blazor.Templates.Client.RazorComponentCodeBehindTemplate", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Api.Components.Pages.Product
{
    public partial class ProductAddPage
    {
        public List<BrandDto>? BrandsModels { get; set; }
        public CreateProductModel Model { get; set; } = new();
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        public IScopedMediator Mediator { get; set; } = default!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = default!;

        private MudForm? _form;
        private bool _saving;

        protected override async Task OnInitializedAsync()
        {
            await LoadBrands();
        }

        private async Task LoadBrands()
        {
            try
            {
                BrandsModels = await Mediator.Send(new GetBrandsQuery());
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Error);
            }
        }

        private async Task CreateProduct()
        {
            try
            {
                await Mediator.Send(new CreateProductCommand(
                    brandId: Model.BrandId.Value,
                    name: Model.Name,
                    description: Model.Description,
                    code: Model.Code));
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Error);
            }
        }

        private void NavigateToProductListPage()
        {
            NavigationManager.NavigateTo("product/list");
        }

        private async Task SaveAsync()
        {
            if (_form is null)
            {
                return;
            }

            await _form.Validate();
            if (!_form.IsValid)
            {
                Snackbar.Add("Please fix validation errors before saving.", Severity.Warning);
                return;
            }

            _saving = true;
            try
            {
                await CreateProduct();
                Snackbar.Add("Product created successfully.", Severity.Success);
                NavigateToProductListPage();
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Failed to save product: {ex.Message}", Severity.Error);
            }
            finally
            {
                _saving = false;
            }
        }

        public class CreateProductModel
        {
            [Required]
            public Guid? BrandId { get; set; }

            [Required]
            public string Name { get; set; } = string.Empty;

            [Required]
            public string Description { get; set; } = string.Empty;

            [Required]
            public string Code { get; set; } = string.Empty;
        }
    }
}
