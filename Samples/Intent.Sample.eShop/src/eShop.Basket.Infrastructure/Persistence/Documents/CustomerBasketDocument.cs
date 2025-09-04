using System;
using System.Collections.Generic;
using System.Linq;
using eShop.Basket.Domain.Entities;
using eShop.Basket.Domain.Repositories.Documents;
using Intent.RoslynWeaver.Attributes;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.MongoDb.MongoDbDocument", Version = "1.0")]

namespace eShop.Basket.Infrastructure.Persistence.Documents
{
    internal class CustomerBasketDocument : ICustomerBasketDocument, IMongoDbDocument<CustomerBasket, CustomerBasketDocument, string>
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string BuyerId { get; set; }
        public IEnumerable<IBasketItemDocument> BasketItems { get; set; }

        public CustomerBasket ToEntity(CustomerBasket? entity = default)
        {
            entity ??= ReflectionHelper.CreateNewInstanceOf<CustomerBasket>();

            entity.BuyerId = BuyerId ?? throw new Exception($"{nameof(entity.BuyerId)} is null");
            entity.BasketItems = BasketItems.Select(x => (x as BasketItemDocument).ToEntity()).ToList();

            return entity;
        }

        public CustomerBasketDocument PopulateFromEntity(CustomerBasket entity)
        {
            BuyerId = entity.BuyerId;
            BasketItems = entity.BasketItems.Select(x => BasketItemDocument.FromEntity(x)!).ToList();

            return this;
        }

        public static CustomerBasketDocument? FromEntity(CustomerBasket? entity)
        {
            if (entity is null)
            {
                return null;
            }

            return new CustomerBasketDocument().PopulateFromEntity(entity);
        }

        public static FilterDefinition<CustomerBasketDocument> GetIdFilter(string buyerId)
        {
            return Builders<CustomerBasketDocument>.Filter.Eq(d => d.BuyerId, buyerId);
        }

        public FilterDefinition<CustomerBasketDocument> GetIdFilter() => GetIdFilter(BuyerId);

        public static FilterDefinition<CustomerBasketDocument> GetIdsFilter(string[] buyerIds)
        {
            return Builders<CustomerBasketDocument>.Filter.In(d => d.BuyerId, buyerIds);
        }
    }
}