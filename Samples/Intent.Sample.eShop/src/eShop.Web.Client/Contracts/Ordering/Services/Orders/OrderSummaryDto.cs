using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.Blazor.HttpClients.DtoContract", Version = "2.0")]

namespace eShop.Web.Client.Contracts.Ordering.Services.Orders
{
    public class OrderSummaryDto
    {
        public OrderSummaryDto()
        {
            Date = null!;
            Status = null!;
        }

        public int OrderNumber { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
        public decimal Total { get; set; }

        public static OrderSummaryDto Create(int orderNumber, string date, string status, decimal total)
        {
            return new OrderSummaryDto
            {
                OrderNumber = orderNumber,
                Date = date,
                Status = status,
                Total = total
            };
        }
    }
}