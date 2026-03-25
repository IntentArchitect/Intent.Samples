using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Application.Customers;
using Intent.Sample.Blazor.Server.Application.Customers.GetCustomerById;
using Intent.Sample.Blazor.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.Blazor.Templates.Client.RazorComponentCodeBehindTemplate", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Api.Components.Pages.Customer
{
    public partial class CustomerViewPage
    {
        [Parameter]
        public Guid CustomerId { get; set; }
        public CustomerDto? CustomerByIdModels { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        public IScopedMediator Mediator { get; set; } = default!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await LoadCustomerById(CustomerId);
        }

        private async Task LoadCustomerById(Guid id)
        {
            try
            {
                CustomerByIdModels = await Mediator.Send(new GetCustomerByIdQuery(
                    id: id));
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Error);
            }
        }

        private void NavigateToCustomerListPage()
        {
            NavigationManager.NavigateTo("customer/list");
        }
    }
}
