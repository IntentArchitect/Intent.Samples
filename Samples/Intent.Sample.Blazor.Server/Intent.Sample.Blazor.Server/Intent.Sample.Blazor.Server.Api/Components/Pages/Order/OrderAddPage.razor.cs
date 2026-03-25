using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Application.Common.Pagination;
using Intent.Sample.Blazor.Server.Application.Customers;
using Intent.Sample.Blazor.Server.Application.Customers.GetCustomers;
using Intent.Sample.Blazor.Server.Application.Orders;
using Intent.Sample.Blazor.Server.Application.Orders.CreateOrder;
using Intent.Sample.Blazor.Server.Application.Products;
using Intent.Sample.Blazor.Server.Application.Products.GetProducts;
using Intent.Sample.Blazor.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.Blazor.Templates.Client.RazorComponentCodeBehindTemplate", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Api.Components.Pages.Order
{
    public partial class OrderAddPage
    {
        public CreateOrderModel Model { get; set; } = new();
        public PagedResult<CustomerSummaryDto> CustomersModels { get; set; }
        public List<ProductDto>? ProductsModels { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        public IScopedMediator Mediator { get; set; } = default!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = default!;

        private MudForm? _form;
        private bool _saving;

        protected override async Task OnInitializedAsync()
        {
            if (Model == null)
            {
                Model = new CreateOrderModel();
            }

            Model.OrderItems ??= new List<CreateOrderCommandOrderItemsModel>();
            if (Model.OrderItems.Count == 0)
            {
                Model.OrderItems.Add(new CreateOrderCommandOrderItemsModel());
            }

            if (!Model.OrderedDate.HasValue)
            {
                Model.OrderedDate = DateTime.Today;
            }

            await LoadCustomers(1, 50, null, null, true);
            await LoadProducts();
        }

        private async Task SaveAsync()
        {
            if (_form == null)
            {
                return;
            }

            await _form.Validate();
            if (!_form.IsValid)
            {
                Snackbar.Add("Please fix validation errors before saving.", Severity.Warning);
                return;
            }

            if (Model.OrderItems != null && Model.OrderItems.Count > 0)
            {
                var validItems = Model.OrderItems.Where(x => x.Units.HasValue && x.Units.Value > 0 && x.Price.HasValue && x.Price.Value >= 0).ToList();
                Model.Total = validItems.Sum(x => x.Units!.Value * x.Price!.Value);
            }

            if (!Model.OrderedDate.HasValue)
            {
                Model.OrderedDate = DateTime.Today;
            }

            _saving = true;
            try
            {
                await CreateOrder();
                Snackbar.Add("Order created successfully.", Severity.Success);
                NavigateToOrderListPage();
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Failed to create order: {ex.Message}", Severity.Error);
            }
            finally
            {
                _saving = false;
            }
        }

        public void AddOrderItem()
        {
            Model.OrderItems ??= new List<CreateOrderCommandOrderItemsModel>();
            Model.OrderItems.Add(new CreateOrderCommandOrderItemsModel());

            CalculateTotal();
        }

        public void RemoveOrderItem(int index)
        {
            if (Model.OrderItems == null)
            {
                return;
            }

            if (index >= 0 && index < Model.OrderItems.Count)
            {
                Model.OrderItems.RemoveAt(index);
            }

            CalculateTotal();
        }

        private void CalculateTotal()
        {
            if (Model.OrderItems != null && Model.OrderItems.Count > 0)
            {
                Model.Total = Model.OrderItems.Where(x => x.Units.HasValue && x.Units.Value > 0 && x.Price.HasValue && x.Price.Value >= 0)
                    .Sum(x => x.Units!.Value * x.Price!.Value);
            }
            else
            {
                Model.Total = 0;
            }
        }

        private async Task CreateOrder()
        {
            try
            {
                await Mediator.Send(new CreateOrderCommand(
                    customerId: Model.CustomerId.Value,
                    orderNo: Model.OrderNo,
                    discountCode: Model.DiscountCode,
                    total: Model.Total.Value,
                    orderedDate: Model.OrderedDate.Value,
                    orderItems: Model.OrderItems
                        .Select(oi => new CreateOrderCommandOrderItemsDto
                        {
                            ProductId = oi.ProductId.Value,
                            Units = oi.Units.Value,
                            Price = oi.Price.Value
                        })
                        .ToList()));
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Error);
            }
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

        private void NavigateToOrderListPage()
        {
            NavigationManager.NavigateTo("order/list");
        }

        public class CreateOrderModel
        {
            public Guid? CustomerId { get; set; }
            public string OrderNo { get; set; }
            public string DiscountCode { get; set; }
            public decimal? Total { get; set; }
            public DateTime? OrderedDate { get; set; }
            public List<CreateOrderCommandOrderItemsModel> OrderItems { get; set; }
        }
        public class CreateOrderCommandOrderItemsModel
        {
            public Guid? ProductId { get; set; }
            public int? Units { get; set; }
            public decimal? Price { get; set; }
        }
    }
}
