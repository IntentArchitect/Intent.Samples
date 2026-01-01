using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Angular.Backend.Application.Common.Interfaces;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Orders.CreateOrder
{
    public class CreateOrderCommand : IRequest<Guid>, ICommand
    {
        public CreateOrderCommand(Guid customerId,
            string orderNo,
            string discountCode,
            decimal total,
            DateTime orderedDate,
            List<CreateOrderCommandOrderItemsDto> orderItems)
        {
            CustomerId = customerId;
            OrderNo = orderNo;
            DiscountCode = discountCode;
            Total = total;
            OrderedDate = orderedDate;
            OrderItems = orderItems;
        }

        public Guid CustomerId { get; set; }
        public string OrderNo { get; set; }
        public string DiscountCode { get; set; }
        public decimal Total { get; set; }
        public DateTime OrderedDate { get; set; }
        public List<CreateOrderCommandOrderItemsDto> OrderItems { get; set; }
    }
}