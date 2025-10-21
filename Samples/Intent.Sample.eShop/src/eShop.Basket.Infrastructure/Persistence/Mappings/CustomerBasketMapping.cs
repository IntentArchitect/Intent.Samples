using eShop.Basket.Domain.Entities;
using Intent.RoslynWeaver.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.MongoDb.MongoDbMapping", Version = "1.0")]

namespace eShop.Basket.Infrastructure.Persistence.Mappings
{
    public class CustomerBasketMapping : IMongoMappingConfiguration<CustomerBasket>
    {
        public string CollectionName => "CustomerBaskets";

        public void RegisterCollectionMap()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(CustomerBasket)))
            {
                BsonClassMap.RegisterClassMap<CustomerBasket>(
                    mapping =>
                    {
                        mapping.AutoMap();
                        mapping.MapIdMember(x => x.BuyerId).SetIdGenerator(StringObjectIdGenerator.Instance);
                    });
            }
        }
    }
}