using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Application.Orders;
using Intent.Sample.Blazor.Server.Application.Orders.GetOrderById;
using Intent.Sample.Blazor.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.Blazor.Templates.Client.RazorComponentCodeBehindTemplate", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Api.Components.Pages.Order
{
    public partial class OrderViewPage
    {
        [Parameter]
        public Guid OrderId { get; set; }
        public OrderDto? OrderByIdModels { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        public IScopedMediator Mediator { get; set; } = default!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await LoadOrderById(OrderId);
        }

        private async Task LoadOrderById(Guid id)
        {
            try
            {
                OrderByIdModels = await Mediator.Send(new GetOrderByIdQuery(
                    id: id));
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Error);
            }
        }

        private void NavigateToOrderListPage()
        {
            NavigationManager.NavigateTo("order/list");
        }
    }
}
