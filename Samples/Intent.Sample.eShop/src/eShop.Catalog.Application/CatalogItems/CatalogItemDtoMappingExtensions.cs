using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using AutoMapper;
using eShop.Catalog.Domain.Entities;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.AutoMapper.MappingExtensions", Version = "1.0")]

namespace eShop.Catalog.Application.CatalogItems
{
    public static class CatalogItemDtoMappingExtensions
    {
        public static CatalogItemDto MapToCatalogItemDto(this CatalogItem projectFrom, IMapper mapper)
            => mapper.Map<CatalogItemDto>(projectFrom);

        public static List<CatalogItemDto> MapToCatalogItemDtoList(this IEnumerable<CatalogItem> projectFrom, IMapper mapper)
            => projectFrom.Select(x => x.MapToCatalogItemDto(mapper)).ToList();
    }
}