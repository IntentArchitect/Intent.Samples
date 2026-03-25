using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Application.Customers;
using Intent.Sample.Blazor.Server.Application.Customers.GetAllCustomers;
using Intent.Sample.Blazor.Server.Application.Customers.DeleteCustomer;
using Intent.Sample.Blazor.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.Blazor.Templates.Client.RazorComponentCodeBehindTemplate", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Api.Components.Pages.Customer
{
    public partial class AllCustomersPage
    {
        public List<CustomerSummaryDto>? AllCustomersModels { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        public IScopedMediator Mediator { get; set; } = default!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await LoadAllCustomers();
        }

        private async Task LoadAllCustomers()
        {
            try
            {
                AllCustomersModels = await Mediator.Send(new GetAllCustomersQuery());
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Error);
            }
        }

        private async Task DeleteCustomer(Guid customerId)
        {
            try
            {
                await Mediator.Send(new DeleteCustomerCommand(
                    id: customerId));
                Snackbar.Add("Customer deleted successfully.", Severity.Success);
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Error);
            }
        }

        private void NavigateToCustomerAddPage()
        {
            NavigationManager.NavigateTo("customer/customer-add-page");
        }

        private void NavigateToCustomerEditPage(Guid customerId)
        {
            NavigationManager.NavigateTo($"customer/edit/{customerId}");
        }

        private void NavigateToCustomerViewPage(Guid customerId)
        {
            NavigationManager.NavigateTo($"customer/view/{customerId}");
        }

        /// <summary>
        /// This is how we do confirmation dialogs for example for deletes, using DialogService.ShowMessageBox.(IMPORTANT)
        /// </summary>
        private async void OnDeleteCustomer(Guid customerId)
        {
            var confirmed = await DialogService.ShowMessageBox(
                title: "Delete customer",
                markupMessage: (MarkupString)"Are you sure you want to delete this customer?",
                yesText: "Delete",
                cancelText: "Cancel",
                options: new DialogOptions { MaxWidth = MaxWidth.ExtraSmall, CloseOnEscapeKey = false, BackdropClick = false }
            );
            if (confirmed == true)
            {
                await DeleteCustomer(customerId);
                await LoadAllCustomers();
                StateHasChanged();
            }
        }

        public class DeleteCustomerModel
        {
            public Guid? Id { get; set; }
        }
    }
}
