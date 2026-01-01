using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Orders
{
    public class OrderSummaryDto
    {
        public OrderSummaryDto()
        {
            OrderNo = null!;
            CustomerName = null!;
            CustomerSurname = null!;
            CustomerEmail = null!;
            TitleName = null!;
        }

        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string OrderNo { get; set; }
        public decimal Total { get; set; }
        public DateTime OrderedDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerEmail { get; set; }
        public string TitleName { get; set; }

        public static OrderSummaryDto Create(
            Guid id,
            Guid customerId,
            string orderNo,
            decimal total,
            DateTime orderedDate,
            string customerName,
            string customerSurname,
            string customerEmail,
            string titleName)
        {
            return new OrderSummaryDto
            {
                Id = id,
                CustomerId = customerId,
                OrderNo = orderNo,
                Total = total,
                OrderedDate = orderedDate,
                CustomerName = customerName,
                CustomerSurname = customerSurname,
                CustomerEmail = customerEmail,
                TitleName = titleName
            };
        }
    }
}