using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using eShop.Basket.Domain.Entities;
using eShop.Basket.Domain.Repositories;
using eShop.Basket.Infrastructure.Persistence;
using Intent.RoslynWeaver.Attributes;
using MongoDB.Driver;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.MongoDb.MongoDbRepository", Version = "1.0")]

namespace eShop.Basket.Infrastructure.Repositories
{
    internal class CustomerBasketMongoRepository : MongoRepositoryBase<CustomerBasket, string>, ICustomerBasketRepository
    {
        public CustomerBasketMongoRepository(IMongoCollection<CustomerBasket> collection, MongoDbUnitOfWork unitOfWork) : base(collection, unitOfWork, x => x.BuyerId)
        {
        }

        public Task<CustomerBasket?> FindByIdAsync(string buyerId, CancellationToken cancellationToken = default) => FindAsync(x => x.BuyerId == buyerId, cancellationToken);

        public Task<List<CustomerBasket>> FindByIdsAsync(string[] buyerIds, CancellationToken cancellationToken = default) => FindAllAsync(x => buyerIds.Contains(x.BuyerId), cancellationToken);
    }
}