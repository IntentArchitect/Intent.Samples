using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Application.Orders
{
    public record OrderSummaryDto
    {
        public OrderSummaryDto()
        {
            OrderNo = null!;
            CustomerName = null!;
            CustomerSurname = null!;
            CustomerEmail = null!;
            TitleName = null!;
        }

        public Guid Id { get; init; }
        public Guid CustomerId { get; init; }
        public string OrderNo { get; init; }
        public decimal Total { get; init; }
        public DateTime OrderedDate { get; init; }
        public string CustomerName { get; init; }
        public string CustomerSurname { get; init; }
        public string CustomerEmail { get; init; }
        public string TitleName { get; init; }
    }
}