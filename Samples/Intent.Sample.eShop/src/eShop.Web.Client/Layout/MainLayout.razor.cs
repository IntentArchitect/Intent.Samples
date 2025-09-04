using System;
using System.Linq;
using System.Threading.Tasks;
using eShop.Web.Client.Services;
using Intent.RoslynWeaver.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.Blazor.Templates.Client.RazorLayoutCodeBehindTemplate", Version = "1.0")]

namespace eShop.Web.Client.Layout
{
    public partial class MainLayout
    {
        [IntentIgnore]
        private bool isBadgeVisible;
        [IntentIgnore]
        private int badgeCount;

        [IntentIgnore]
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [IntentIgnore]
        [Inject] AuthenticationStateProvider AuthenticationStateProvider { get; set; }


        [IntentIgnore]
        [Inject]
        public IBasketService BasketService { get; set; } = default!;

        [IntentIgnore]
        [Inject]
        public BasketState BasketState { get; set; }

        protected override async Task OnInitializedAsync()
        {
            
        }

        private void BasketState_OnCountChanged(int count)
        {
            badgeCount = count;
            if (badgeCount > 0)
            {
                isBadgeVisible = true;
            }
            StateHasChanged();
        }

        [IntentIgnore]
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await InitializeComponentAsync();
            }
        }

        [IntentIgnore]
        private async Task InitializeComponentAsync()
        {
            await BasketState.InitializeAsync();
            BasketState.OnCountChanged += BasketState_OnCountChanged;

            var authed = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            if (authed.User.Identity is not null && authed.User.Identity.IsAuthenticated)
            {
                badgeCount = await GetCount();
                if (badgeCount > 0)
                {
                    isBadgeVisible = true;
                }
                StateHasChanged();
            }
        }

        [IntentIgnore]
        private void RedirectToLogin()
        {
            NavigationManager.NavigateTo($"Account/Login?returnUrl={Uri.EscapeDataString(NavigationManager.Uri)}", forceLoad: true);
        }

        [IntentIgnore]
        private void RedirectToLogout()
        {
            NavigationManager.NavigateTo($"Account/Logout", forceLoad: true);
        }

        [IntentIgnore]
        private async Task<int> GetCount()
        {

            var basket = await BasketService.GetBasketByIdAsync(BasketState.Id);

            return basket.BasketItems.Sum(i => i.Quantity);
        }

        [IntentIgnore]
        private async Task<bool> IsBadgeVisible()
        {
            return await GetCount() > 0;
        }
    }
}