using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Application.Products;
using Intent.Sample.Blazor.Server.Application.Products.GetProducts;
using Intent.Sample.Blazor.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.Blazor.Templates.Client.RazorComponentCodeBehindTemplate", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Api.Components.Pages.Product
{
    public partial class ProductListPage
    {
        public List<ProductDto>? ProductsModels { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        public IScopedMediator Mediator { get; set; } = default!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            try
            {
                ProductsModels = await Mediator.Send(new GetProductsQuery());
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Error);
            }
        }

        private void NavigateToProductAddPage()
        {
            NavigationManager.NavigateTo("product/add");
        }

        private void NavigateToProductEditPage(Guid productId)
        {
            NavigationManager.NavigateTo($"product/edit/{productId}");
        }
    }
}
