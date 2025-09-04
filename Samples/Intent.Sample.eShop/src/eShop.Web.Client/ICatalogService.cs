using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using eShop.Web.Client.Common;
using eShop.Web.Client.Contracts.Catalog.Services.CatalogItems;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.Blazor.HttpClients.ServiceContract", Version = "2.0")]

namespace eShop.Web.Client
{
    public interface ICatalogService : IDisposable
    {
        Task<List<CatalogBrandDto>> CatalogBrandsAsync(CancellationToken cancellationToken = default);
        Task<List<CatalogTypeDto>> CatalogTypesAsync(CancellationToken cancellationToken = default);
        Task<CatalogItemDto> ItemByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<PagedResult<CatalogItemDto>> ItemsByTypeIdAndBrandIdAsync(int catalogTypeId, int? catalogBrandId, int pageSize, int pageIndex, CancellationToken cancellationToken = default);
    }
}