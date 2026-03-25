using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Application.Orders;
using Intent.Sample.Blazor.Server.Application.Orders.GetOrderById;
using Intent.Sample.Blazor.Server.Application.Orders.UpdateOrder;
using Intent.Sample.Blazor.Server.Application.Products;
using Intent.Sample.Blazor.Server.Application.Products.GetProducts;
using Intent.Sample.Blazor.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.Blazor.Templates.Client.RazorComponentCodeBehindTemplate", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Api.Components.Pages.Order
{
    public partial class OrderEditPage
    {
        public UpdateOrderModel? Model { get; set; }
        public List<ProductDto>? ProductsModels { get; set; }
        [Parameter]
        public Guid OrderId { get; set; }
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
            await LoadOrderById(OrderId);
            await LoadProducts();
        }

        private async Task UpdateOrder()
        {
            try
            {
                await Mediator.Send(new UpdateOrderCommand(
                    id: Model.Id.Value,
                    customerId: Model.CustomerId.Value,
                    orderNo: Model.OrderNo,
                    discountCode: Model.DiscountCode,
                    total: Model.Total.Value,
                    orderedDate: Model.OrderedDate.Value,
                    orderItems: Model.OrderItems
                        .Select(oi => new UpdateOrderCommandOrderItemsDto
                        {
                            Id = oi.Id.Value,
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

        private async Task LoadOrderById(Guid id)
        {
            try
            {
                var orderDto = await Mediator.Send(new GetOrderByIdQuery(
                    id: id));
                Model = new UpdateOrderModel
                {
                    Id = orderDto.Id,
                    CustomerId = orderDto.CustomerId,
                    OrderNo = orderDto.OrderNo,
                    DiscountCode = orderDto.DiscountCode,
                    Total = orderDto.Total,
                    OrderedDate = orderDto.OrderedDate,
                    OrderItems = orderDto.OrderItems
                        .Select(oi => new UpdateOrderCommandOrderItemsModel
                        {
                            Id = oi.Id,
                            ProductId = oi.ProductId,
                            Units = oi.Units,
                            Price = oi.Price
                        })
                        .ToList(),
                    CustomerName = orderDto.CustomerName,
                    CustomerSurname = orderDto.CustomerSurname,
                    Email = orderDto.CustomerEmail
                };
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

        private async Task SaveAsync()
        {
            if (_form is null)
            {
                return;
            }

            await _form.Validate();
            if (!_form.IsValid)
            {
                Snackbar.Add("Please fix validation errors before saving.", Severity.Warning);
                return;
            }

            if (Model is null)
            {
                return;
            }

            _saving = true;
            try
            {
                RecalculateTotal();
                await UpdateOrder();
                Snackbar.Add("Order updated successfully.", Severity.Success);
                NavigateToOrderListPage();
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Failed to save order: {ex.Message}", Severity.Error);
            }
            finally
            {
                _saving = false;
            }
        }

        private void Cancel()
        {
            NavigateToOrderListPage();
        }

        private void AddOrderItem()
        {
            if (Model == null)
            {
                return;
            }

            if (Model.OrderItems == null)
            {
                Model.OrderItems = new List<UpdateOrderCommandOrderItemsModel>();
            }

            Model.OrderItems.Add(new UpdateOrderCommandOrderItemsModel
            {
                Id = Guid.NewGuid(),
                ProductId = null,
                Units = null,
                Price = null
            });
        }

        private void RemoveOrderItem(int index)
        {
            if (Model == null || Model.OrderItems == null)
            {
                return;
            }

            if (index < 0 || index >= Model.OrderItems.Count)
            {
                return;
            }

            Model.OrderItems.RemoveAt(index);
            RecalculateTotal();
        }

        private void RecalculateTotal()
        {
            if (Model == null || Model.OrderItems == null || Model.OrderItems.Count == 0)
            {
                Model.Total = 0;
                return;
            }

            decimal total = 0;
            foreach (var item in Model.OrderItems)
            {
                if (item.Units.HasValue && item.Price.HasValue)
                {
                    total += item.Units.Value * item.Price.Value;
                }
            }

            Model.Total = total;
        }

        public class UpdateOrderModel
        {
            public Guid? Id { get; set; }
            public Guid? CustomerId { get; set; }
            public string OrderNo { get; set; }
            public string DiscountCode { get; set; }
            public decimal? Total { get; set; }
            public DateTime? OrderedDate { get; set; }
            public List<UpdateOrderCommandOrderItemsModel> OrderItems { get; set; }
            public string CustomerName { get; set; }
            public string CustomerSurname { get; set; }
            public string Email { get; set; }
        }
        public class UpdateOrderCommandOrderItemsModel
        {
            public Guid? Id { get; set; }
            public Guid? ProductId { get; set; }
            public int? Units { get; set; }
            public decimal? Price { get; set; }
        }
    }
}
