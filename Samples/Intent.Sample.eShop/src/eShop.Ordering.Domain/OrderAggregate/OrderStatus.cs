using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Entities.DomainEnum", Version = "1.0")]

namespace eShop.Ordering.Domain.OrderAggregate
{
    public enum OrderStatus
    {
        Submitted = 1,
        AwaitingValidation = 2,
        StockConfirmed = 3,
        Paid = 4,
        Shipped = 5,
        Cancelled = 6
    }
}