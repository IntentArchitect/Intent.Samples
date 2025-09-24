using System;
using System.Linq;
using System.Threading.Tasks;
using eShop.Web.Client.Contracts.Basket.Services.CustomerBaskets;
using eShop.Web.Client.Pages.Cart.Components;
using eShop.Web.Client.Services;
using Intent.RoslynWeaver.Attributes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.Blazor.Templates.Client.RazorComponentCodeBehindTemplate", Version = "1.0")]

namespace eShop.Web.Client.Pages.Cart
{
    public partial class Cart
    {
        public MudDataGrid<BasketDto> DataGrid { get; set; }
        public BasketDto Model { get; set; }
        [IntentIgnore][Inject] public ProductImageUrlProvider ProductImages { get; set; }
        public int TotalQuantity { get => this.Model is null ? 0 : this.Model.BasketItems.Sum(x => x.Quantity); }
        public decimal TotalPrice { get => this.Model is null ? 0 : this.Model.BasketItems.Sum(x => x.UnitPrice * x.Quantity); }
        [IntentIgnore][Inject] public BasketState BasketState { get; set; }
        [IntentIgnore] public bool CheckoutDisabled { get => TotalPrice < 0; }
        [Inject]
        public IBasketService BasketService { get; set; } = default!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = default!;

        [IntentIgnore]
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
                Model = await BasketService.GetBasketByIdAsync(BasketState.Id);
                StateHasChanged();
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Error);
            }
        }

        [IntentIgnore]
        private int CurrentOrPendingQuantity(int productId, int quantity)
        {
            return productId * quantity;
        }

        private async Task SubmitUpdateAsync(int quantity, int productId)
        {
            try
            {
                Model.BasketItems.First(p => p.ProductId == productId).Quantity = quantity;
                await BasketService.UpdateBasketAsync(new BasketDto { BuyerId = BasketState.Id, BasketItems = Model.BasketItems });
                await InitializeComponentAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [IntentIgnore]
        private decimal GetTotal()
        {
            return TotalQuantity;
        }
    }
}