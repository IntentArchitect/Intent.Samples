using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace Intent.Sample.Blazor.Server.Domain.Entities
{
    public class OrderItem
    {
        public OrderItem()
        {
            Product = null!;
        }

        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public int Units { get; set; }

        public decimal Price { get; set; }

        public virtual Product Product { get; set; }
    }
}