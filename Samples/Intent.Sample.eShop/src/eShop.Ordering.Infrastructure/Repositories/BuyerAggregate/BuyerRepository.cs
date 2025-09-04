using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eShop.Ordering.Domain.BuyerAggregate;
using eShop.Ordering.Domain.Repositories;
using eShop.Ordering.Domain.Repositories.BuyerAggregate;
using eShop.Ordering.Infrastructure.Persistence;
using Intent.RoslynWeaver.Attributes;
using Microsoft.EntityFrameworkCore;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.Repositories.Repository", Version = "1.0")]

namespace eShop.Ordering.Infrastructure.Repositories.BuyerAggregate
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class BuyerRepository : RepositoryBase<Buyer, Buyer, ApplicationDbContext>, IBuyerRepository
    {
        public BuyerRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<TProjection?> FindByIdProjectToAsync<TProjection>(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await FindProjectToAsync<TProjection>(x => x.Id == id, cancellationToken);
        }

        public async Task<Buyer?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await FindAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<Buyer?> FindByIdAsync(
            int id,
            Func<IQueryable<Buyer>, IQueryable<Buyer>> queryOptions,
            CancellationToken cancellationToken = default)
        {
            return await FindAsync(x => x.Id == id, queryOptions, cancellationToken);
        }

        public async Task<List<Buyer>> FindByIdsAsync(int[] ids, CancellationToken cancellationToken = default)
        {
            // Force materialization - Some combinations of .net9 runtime and EF runtime crash with "Convert ReadOnlySpan to List since expression trees can't handle ref struct"
            var idList = ids.ToList();
            return await FindAllAsync(x => idList.Contains(x.Id), cancellationToken);
        }

        public async Task<Buyer> FindByUserIdAsync(string userId, CancellationToken cancellationToken)
        {
            return await FindAsync(x => x.BuyerIdentifier == userId, cancellationToken);
        }
    }
}