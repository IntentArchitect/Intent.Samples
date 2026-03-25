using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace Intent.Sample.Blazor.Server.Domain.Entities
{
    public class Order
    {
        public Order()
        {
            OrderNo = null!;
            DiscountCode = null!;
            Customer = null!;
        }

        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public string OrderNo { get; set; }

        public string DiscountCode { get; set; }

        public decimal Total { get; set; }

        public DateTime OrderedDate { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = [];

        public virtual Customer Customer { get; set; }
    }
}