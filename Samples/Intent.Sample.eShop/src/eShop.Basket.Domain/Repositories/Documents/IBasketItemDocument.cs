using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.MongoDb.MongoDbDocumentInterface", Version = "1.0")]

namespace eShop.Basket.Domain.Repositories.Documents
{
    public interface IBasketItemDocument
    {
        string Id { get; }
        int ProductId { get; }
        string ProductName { get; }
        decimal UnitPrice { get; }
        decimal OldUnitPrice { get; }
        int Quantity { get; }
        string PictureUrl { get; }
    }
}