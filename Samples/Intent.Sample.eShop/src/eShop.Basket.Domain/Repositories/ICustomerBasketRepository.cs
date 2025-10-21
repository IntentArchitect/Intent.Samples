using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using eShop.Basket.Domain.Entities;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Entities.Repositories.Api.EntityRepositoryInterface", Version = "1.0")]

namespace eShop.Basket.Domain.Repositories
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public interface ICustomerBasketRepository : IMongoRepository<CustomerBasket, string>
    {
        [IntentManaged(Mode.Fully)]
        Task<CustomerBasket?> FindByIdAsync(string buyerId, CancellationToken cancellationToken = default);
        [IntentManaged(Mode.Fully)]
        Task<List<CustomerBasket>> FindByIdsAsync(string[] buyerIds, CancellationToken cancellationToken = default);
    }
}