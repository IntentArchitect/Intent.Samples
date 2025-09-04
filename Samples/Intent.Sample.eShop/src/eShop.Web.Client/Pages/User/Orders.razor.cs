using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using eShop.Web.Client.Contracts.Ordering.Services.Orders;
using eShop.Web.Client.Services;
using Intent.RoslynWeaver.Attributes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.Blazor.Templates.Client.RazorComponentCodeBehindTemplate", Version = "1.0")]

namespace eShop.Web.Client.Pages.User
{
    public partial class Orders
    {
        public List<OrderSummaryDto> Model { get; set; }
        [Inject]
        public IOrdersService OrdersService { get; set; } = default!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = default!;
        protected override async Task OnInitializedAsync()
        {
        }

        [IntentIgnore]
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await InitializeComponentAsync();
                StateHasChanged();
            }
        }

        private async Task InitializeComponentAsync()
        {
            try
            {
                Model = await OrdersService.GetOrdersAsync();
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Error);
            }
        }

        [IntentIgnore]
        private string GetStatus(string status)
        {
            switch (status)
            {
                case "2": return "Awaiting Validation";
                case "3": return "Stock Confirmed";
                case "4": return "Paid";
                case "5": return "Shipped";
                case "6": return "Cancelled";
                default: return "Submitted";
            }
        }
    }
}