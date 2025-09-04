using System;
using System.Linq;
using System.Threading.Tasks;
using eShop.Web.Client.Contracts.Basket.Services.CustomerBaskets;
using eShop.Web.Client.Contracts.Catalog.Services.CatalogItems;
using eShop.Web.Client.Services;
using Intent.RoslynWeaver.Attributes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.Blazor.Templates.Client.RazorComponentCodeBehindTemplate", Version = "1.0")]

namespace eShop.Web.Client.Pages
{
    public partial class Item
    {
        private bool _addToCartProcessing = false;
        [Parameter]
        public int ProductId { get; set; }
        public CatalogItemDto? Model { get; set; }
        [IntentIgnore]
        [Inject]
        public BasketState BasketState { get; set; }
        public BasketDto Basket { get; set; }
        [Inject]
        public ICatalogService CatalogService { get; set; } = default!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = default!;
        [Inject]
        public IBasketService BasketService { get; set; } = default!;

        [IntentIgnore]
        [Inject] ProductImageUrlProvider ProductImageUrlProvider { get; set; }

        [IntentIgnore]
        [Inject] NavigationManager NavigationManager { get; set; }

        [IntentIgnore]
        [Inject] AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        protected override async Task OnInitializedAsync()
        {
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await InitializeComponentAsync();
            }
        }

        private async Task InitializeComponentAsync()
        {
            try
            {
                Model = await CatalogService.ItemByIdAsync(ProductId);
                var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                if (authState.User.Identity is not null && authState.User.Identity.IsAuthenticated)
                {
                    Basket = await BasketService.GetBasketByIdAsync(BasketState.Id);
                }
                StateHasChanged();
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Error);
                throw;
            }
        }

        [IntentIgnore]
        private async Task AddToCart()
        {
            try
            {
                var currentBasket = await BasketService.GetBasketByIdAsync(BasketState.Id);
                if (currentBasket.BasketItems.Any(i => i.ProductId == ProductId))
                {
                    currentBasket.BasketItems.First(i => i.ProductId == ProductId).Quantity += 1;
                }
                else
                {
                    currentBasket.BasketItems.Add(new BasketItemDto
                    {
                        Id = Guid.NewGuid().ToString(),
                        OldUnitPrice = Model.Price,
                        PictureUrl = Model.PictureUri,
                        ProductId = ProductId,
                        ProductName = Model.Name,
                        Quantity = 1,
                        UnitPrice = Model.Price
                    });
                }

                _addToCartProcessing = true;
                await BasketService.UpdateBasketAsync(currentBasket);
                Basket = await BasketService.GetBasketByIdAsync(BasketState.Id);
                BasketState.BasketUpdate(Basket.BasketItems.Sum(i => i.Quantity));
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Error);
            }
            finally
            {
                _addToCartProcessing = false;
            }
        }

        [IntentIgnore]
        private void RedirectToLogin()
        {
            NavigationManager.NavigateTo($"Account/Login?returnUrl={Uri.EscapeDataString(NavigationManager.Uri)}", forceLoad: true);
        }
    }
}