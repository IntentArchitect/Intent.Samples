using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Application.Orders;
using Intent.Sample.Blazor.Server.Application.Orders.GetOrders;
using Intent.Sample.Blazor.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.Blazor.Templates.Client.RazorComponentCodeBehindTemplate", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Api.Components.Pages.Order
{
    public partial class OrderListPage
    {
        public List<OrderDto>? OrdersModels { get; set; }
        public DeleteOrderModel? Model { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        public IScopedMediator Mediator { get; set; } = default!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = default!;
        [Inject]
        public IDialogService DialogService { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await LoadOrders();
        }

        private async Task LoadOrders()
        {
            try
            {
                OrdersModels = await Mediator.Send(new GetOrdersQuery());
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Error);
            }
        }

        private async Task DeleteOrder(Guid orderId)
        {
            try
            {
                var parameters = new DialogParameters<OrderDeleteDialog>
                {
                    { x => x.OrderId, orderId },
                };
                var dialog = await DialogService.ShowAsync<OrderDeleteDialog>("Delete Order", parameters, new DialogOptions() { FullWidth = true, MaxWidth = MaxWidth.Large, BackdropClick = false });
                var result = await dialog.Result;

                if (result.Canceled)
                {
                    return;
                }
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Error);
            }
        }

        private void NavigateToOrderAddPage()
        {
            NavigationManager.NavigateTo("order/add");
        }

        private void NavigateToOrderEditPage(Guid orderId)
        {
            NavigationManager.NavigateTo($"order/edit/{orderId}");
        }

        private void NavigateToOrderViewPage(Guid orderId)
        {
            NavigationManager.NavigateTo($"order/view/{orderId}");
        }

        private async Task OnDeleteOrder(Guid orderId)
        {
            await DeleteOrder(orderId);
            await LoadOrders();
            StateHasChanged();
        }

        public class DeleteOrderModel
        {
            public Guid? Id { get; set; }
        }
    }
}
