using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Application.Common.Pagination;
using Intent.Sample.Blazor.Server.Application.Customers;
using Intent.Sample.Blazor.Server.Application.Customers.GetCustomers;
using Intent.Sample.Blazor.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.Blazor.Templates.Client.RazorComponentCodeBehindTemplate", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Api.Components.Pages.Customer
{
    public partial class CustomerListPage
    {
        public PagedResult<CustomerSummaryDto> CustomersModels { get; set; }
        [Inject]
        public IScopedMediator Mediator { get; set; } = default!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = default!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        public string? SearchText { get; set; } = string.Empty;
        public bool? IsActive { get; set; } = null;
        private MudTable<CustomerSummaryDto>? _table;

        protected override async Task OnInitializedAsync()
        {
        }

        private async Task LoadCustomers(int pageNo, int pageSize, string? searchTerm, string? orderBy, bool? isActive)
        {
            try
            {
                CustomersModels = await Mediator.Send(new GetCustomersQuery(
                    pageNo: pageNo,
                    pageSize: pageSize,
                    searchTerm: searchTerm,
                    orderBy: orderBy,
                    isActive: isActive));
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

        public async Task<TableData<CustomerSummaryDto>> LoadServerData(TableState state, CancellationToken cancellationToken)
        {
            string? orderBy = null;
            if (!string.IsNullOrEmpty(state.SortLabel))
            {
                orderBy = $"{state.SortLabel} {(state.SortDirection == SortDirection.Descending ? "desc" : "asc")}";
            }

            var pageNo = state.Page + 1;
            var pageSize = state.PageSize;

            await LoadCustomers(pageNo, pageSize, SearchText, orderBy, IsActive);

            var items = CustomersModels?.Data ?? new List<CustomerSummaryDto>();
            var totalItems = CustomersModels?.TotalCount ?? 0;

            return new TableData<CustomerSummaryDto>
            {
                Items = items,
                TotalItems = totalItems
            };
        }
    }
}
