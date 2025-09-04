using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eShop.Catalog.Application.CatalogItems;
using eShop.Catalog.Application.Common.Eventing;
using eShop.Catalog.Application.Common.Pagination;
using eShop.Catalog.Application.Interfaces;
using eShop.Catalog.Domain.Common.Exceptions;
using eShop.Catalog.Domain.Entities;
using eShop.Catalog.Domain.Repositories;
using eShop.Catalog.Eventing.Messages;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.ServiceImplementations.ServiceImplementation", Version = "1.0")]

namespace eShop.Catalog.Application.Implementation
{
    [IntentManaged(Mode.Merge)]
    public class CatalogService : ICatalogService
    {
        private readonly ICatalogItemRepository _catalogItemRepository;
        private readonly IMapper _mapper;
        private readonly ICatalogTypeRepository _catalogTypeRepository;
        private readonly ICatalogBrandRepository _catalogBrandRepository;
        private readonly IEventBus _eventBus;

        [IntentManaged(Mode.Fully, Body = Mode.Merge)]
        public CatalogService(ICatalogItemRepository catalogItemRepository,
            IMapper mapper,
            ICatalogTypeRepository catalogTypeRepository,
            ICatalogBrandRepository catalogBrandRepository,
            IEventBus eventBus)
        {
            _catalogItemRepository = catalogItemRepository;
            _mapper = mapper;
            _catalogTypeRepository = catalogTypeRepository;
            _catalogBrandRepository = catalogBrandRepository;
            _eventBus = eventBus;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<PagedResult<CatalogItemDto>> Items(
            int pageIndex = 0,
            int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            var catalogItems = await _catalogItemRepository.FindAllAsync(pageIndex + 1, pageSize, cancellationToken);
            return catalogItems.MapToPagedResult(x => x.MapToCatalogItemDto(_mapper));
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<CatalogItemDto> ItemById(int id, CancellationToken cancellationToken = default)
        {
            var catalogItem = await _catalogItemRepository.FindByIdAsync(id, cancellationToken);
            if (catalogItem is null)
            {
                throw new NotFoundException($"Could not find CatalogItem '{id}'");
            }
            return catalogItem.MapToCatalogItemDto(_mapper);
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<PagedResult<CatalogItemDto>> ItemsWithName(
            string name,
            int pageSize = 10,
            int pageIndex = 0,
            CancellationToken cancellationToken = default)
        {
            var catalogItems = await _catalogItemRepository.FindAllAsync(x => x.Name.StartsWith(name), pageIndex + 1, pageSize, cancellationToken);
            return catalogItems.MapToPagedResult(x => x.MapToCatalogItemDto(_mapper));
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<PagedResult<CatalogItemDto>> ItemsByTypeIdAndBrandId(
            int catalogTypeId,
            int? catalogBrandId,
            int pageSize = 10,
            int pageIndex = 0,
            CancellationToken cancellationToken = default)
        {
            IQueryable<CatalogItem> FilterCatalogItems(IQueryable<CatalogItem> queryable)
            {
                // IntentIgnore (Match="queryable = queryable.Where(x => x.CatalogTypeId == catalogTypeId);")
                if (catalogTypeId != 0)
                {
                    queryable = queryable.Where(x => x.CatalogTypeId == catalogTypeId);
                }
                // IntentIgnore (Match="if (catalogBrandId.Value != null)")
                if (catalogBrandId.HasValue)
                {
                    queryable = queryable.Where(x => x.CatalogBrandId == catalogBrandId.Value);
                }

                if (catalogBrandId != null)
                {
                    queryable = queryable.Where(x => x.CatalogBrandId == catalogBrandId);
                }
                return queryable;
            }

            var catalogItems = await _catalogItemRepository.FindAllAsync(pageIndex + 1, pageSize, FilterCatalogItems, cancellationToken);
            return catalogItems.MapToPagedResult(x => x.MapToCatalogItemDto(_mapper));
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<PagedResult<CatalogItemDto>> ItemsByBrandId(
            int? catalogBrandId,
            int pageSize = 10,
            int pageIndex = 0,
            CancellationToken cancellationToken = default)
        {
            // IntentIgnore
            Expression<Func<CatalogItem, bool>>? predicate = null;
            IQueryable<CatalogItem> FilterCatalogItems(IQueryable<CatalogItem> queryable)
            {
                if (catalogBrandId != null)
                {
                    queryable = queryable.Where(x => x.CatalogBrandId == catalogBrandId);
                }
                return queryable;
            }

            var catalogItems = await _catalogItemRepository.FindAllAsync(pageIndex + 1, pageSize, FilterCatalogItems, cancellationToken);
            return catalogItems.MapToPagedResult(x => x.MapToCatalogItemDto(_mapper));
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<List<CatalogTypeDto>> CatalogTypes(CancellationToken cancellationToken = default)
        {
            var catalogTypes = await _catalogTypeRepository.FindAllAsync(cancellationToken);
            return catalogTypes.MapToCatalogTypeDtoList(_mapper);
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<List<CatalogBrandDto>> CatalogBrands(CancellationToken cancellationToken = default)
        {
            var catalogBrands = await _catalogBrandRepository.FindAllAsync(cancellationToken);
            return catalogBrands.MapToCatalogBrandDtoList(_mapper);
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task UpdateProduct(CatalogItemUpdateDto dto, int id, CancellationToken cancellationToken = default)
        {
            var catalogItem = await _catalogItemRepository.FindByIdAsync(dto.Id, cancellationToken);
            if (catalogItem is null)
            {
                throw new NotFoundException($"Could not find CatalogItem '{dto.Id}'");
            }

            // IntentIgnore
            var oldPrice = catalogItem.Price;

            catalogItem.CatalogBrandId = dto.CatalogBrandId;
            catalogItem.CatalogTypeId = dto.CatalogTypeId;
            catalogItem.Name = dto.Name;
            catalogItem.Description = dto.Description;
            catalogItem.Price = dto.Price;
            catalogItem.PictureFileName = dto.PictureFileName;
            catalogItem.PictureUri = dto.PictureUri;
            catalogItem.AvailableStock = dto.AvailableStock;
            catalogItem.RestockThreshold = dto.RestockThreshold;
            catalogItem.MaxStockThreshold = dto.MaxStockThreshold;
            catalogItem.OnReorder = dto.OnReorder;
            _eventBus.Publish(new ProductPriceChangedIntegrationEvent
            {
                ProductId = catalogItem.Id,
                NewPrice = catalogItem.Price,
                OldPrice = oldPrice
            });
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<int> CreateProduct(CatalogItemCreateDto dto, CancellationToken cancellationToken = default)
        {
            var catalogItem = new CatalogItem
            {
                CatalogBrandId = dto.CatalogBrandId,
                CatalogTypeId = dto.CatalogTypeId,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                PictureFileName = dto.PictureFileName,
                PictureUri = dto.PictureUri,
                AvailableStock = dto.AvailableStock,
                RestockThreshold = dto.RestockThreshold,
                MaxStockThreshold = dto.MaxStockThreshold,
                OnReorder = dto.OnReorder
            };
            _catalogItemRepository.Add(catalogItem);
            await _catalogItemRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return catalogItem.Id;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task DeleteProduct(int id, CancellationToken cancellationToken = default)
        {
            var catalogItem = await _catalogItemRepository.FindByIdAsync(id, cancellationToken);
            if (catalogItem is null)
            {
                throw new NotFoundException($"Could not find CatalogItem '{id}'");
            }

            _catalogItemRepository.Remove(catalogItem);
        }
    }
}