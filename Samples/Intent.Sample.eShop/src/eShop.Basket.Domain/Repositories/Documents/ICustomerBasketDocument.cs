using System.Collections.Generic;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.MongoDb.MongoDbDocumentInterface", Version = "1.0")]

namespace eShop.Basket.Domain.Repositories.Documents
{
    public interface ICustomerBasketDocument
    {
        string BuyerId { get; }
        IEnumerable<IBasketItemDocument> BasketItems { get; }
    }
}