using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using eShop.Ordering.Domain.BuyerAggregate;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Entities.Repositories.Api.EntityRepositoryInterface", Version = "1.0")]

namespace eShop.Ordering.Domain.Repositories.BuyerAggregate
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public interface IBuyerRepository : IEFRepository<Buyer, Buyer>
    {
        [IntentManaged(Mode.Fully)]
        Task<TProjection?> FindByIdProjectToAsync<TProjection>(int id, CancellationToken cancellationToken = default);
        [IntentManaged(Mode.Fully)]
        Task<Buyer?> FindByIdAsync(int id, CancellationToken cancellationToken = default);
        [IntentManaged(Mode.Fully)]
        Task<Buyer?> FindByIdAsync(int id, Func<IQueryable<Buyer>, IQueryable<Buyer>> queryOptions, CancellationToken cancellationToken = default);
        [IntentManaged(Mode.Fully)]
        Task<List<Buyer>> FindByIdsAsync(int[] ids, CancellationToken cancellationToken = default);
        Task<Buyer?> FindByUserIdAsync(string userId, CancellationToken cancellationToken);
    }
}