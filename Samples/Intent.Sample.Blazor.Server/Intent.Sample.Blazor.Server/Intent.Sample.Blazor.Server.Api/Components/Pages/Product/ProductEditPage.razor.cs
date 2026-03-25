using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Application.Brands;
using Intent.Sample.Blazor.Server.Application.Brands.GetBrands;
using Intent.Sample.Blazor.Server.Application.Products.GetProductById;
using Intent.Sample.Blazor.Server.Application.Products.UpdateProduct;
using Intent.Sample.Blazor.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.Blazor.Templates.Client.RazorComponentCodeBehindTemplate", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Api.Components.Pages.Product
{
    public partial class ProductEditPage
    {
        [Parameter]
        public Guid ProductId { get; set; }
        public List<BrandDto>? BrandsModels { get; set; }
        public UpdateProductModel? Model { get; set; }
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
            await LoadProductById(ProductId);
            StateHasChanged();
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

        private async Task UpdateProduct()
        {
            try
            {
                await Mediator.Send(new UpdateProductCommand(
                    id: Model.Id.Value,
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

        private async Task LoadProductById(Guid id)
        {
            try
            {
                var productDto = await Mediator.Send(new GetProductByIdQuery(
                    id: id));
                Model = new UpdateProductModel
                {
                    Id = productDto.Id,
                    BrandId = productDto.BrandId,
                    Name = productDto.Name,
                    Description = productDto.Description,
                    Code = productDto.Code
                };
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
                await UpdateProduct();
                Snackbar.Add("Product updated successfully.", Severity.Success);
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

        private void Cancel()
        {
            NavigateToProductListPage();
        }

        public class UpdateProductModel
        {
            public Guid? Id { get; set; }
            public Guid? BrandId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Code { get; set; }
        }
    }
}
