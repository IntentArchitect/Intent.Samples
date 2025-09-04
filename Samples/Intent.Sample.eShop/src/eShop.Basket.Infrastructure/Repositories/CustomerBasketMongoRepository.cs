using System;
using System.Linq.Expressions;
using eShop.Basket.Domain.Entities;
using eShop.Basket.Domain.Repositories;
using eShop.Basket.Domain.Repositories.Documents;
using eShop.Basket.Infrastructure.Persistence;
using eShop.Basket.Infrastructure.Persistence.Documents;
using Intent.RoslynWeaver.Attributes;
using MongoDB.Driver;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.MongoDb.MongoDbRepository", Version = "1.0")]

namespace eShop.Basket.Infrastructure.Repositories
{
    internal class CustomerBasketMongoRepository : MongoRepositoryBase<CustomerBasket, CustomerBasketDocument, ICustomerBasketDocument, string>, ICustomerBasketRepository
    {
        public CustomerBasketMongoRepository(IMongoCollection<CustomerBasketDocument> collection,
            MongoDbUnitOfWork unitOfWork) : base(collection, unitOfWork)
        {
        }
    }
}