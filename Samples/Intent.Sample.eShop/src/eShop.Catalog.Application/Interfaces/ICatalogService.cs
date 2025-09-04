using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using eShop.Catalog.Application.CatalogItems;
using eShop.Catalog.Application.Common.Pagination;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Contracts.ServiceContract", Version = "1.0")]

namespace eShop.Catalog.Application.Interfaces
{
    public interface ICatalogService
    {
        Task<PagedResult<CatalogItemDto>> Items(int pageIndex, int pageSize, CancellationToken cancellationToken = default);
        Task<CatalogItemDto> ItemById(int id, CancellationToken cancellationToken = default);
        Task<PagedResult<CatalogItemDto>> ItemsWithName(string name, int pageSize, int pageIndex, CancellationToken cancellationToken = default);
        Task<PagedResult<CatalogItemDto>> ItemsByTypeIdAndBrandId(int catalogTypeId, int? catalogBrandId, int pageSize, int pageIndex, CancellationToken cancellationToken = default);
        Task<PagedResult<CatalogItemDto>> ItemsByBrandId(int? catalogBrandId, int pageSize, int pageIndex, CancellationToken cancellationToken = default);
        Task<List<CatalogTypeDto>> CatalogTypes(CancellationToken cancellationToken = default);
        Task<List<CatalogBrandDto>> CatalogBrands(CancellationToken cancellationToken = default);
        Task UpdateProduct(CatalogItemUpdateDto dto, int id, CancellationToken cancellationToken = default);
        Task<int> CreateProduct(CatalogItemCreateDto dto, CancellationToken cancellationToken = default);
        Task DeleteProduct(int id, CancellationToken cancellationToken = default);
    }
}