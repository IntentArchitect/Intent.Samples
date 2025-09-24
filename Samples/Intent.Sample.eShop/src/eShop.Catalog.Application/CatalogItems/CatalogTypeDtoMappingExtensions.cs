using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using eShop.Catalog.Domain.Entities;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.AutoMapper.MappingExtensions", Version = "1.0")]

namespace eShop.Catalog.Application.CatalogItems
{
    public static class CatalogTypeDtoMappingExtensions
    {
        public static CatalogTypeDto MapToCatalogTypeDto(this CatalogType projectFrom, IMapper mapper)
            => mapper.Map<CatalogTypeDto>(projectFrom);

        public static List<CatalogTypeDto> MapToCatalogTypeDtoList(this IEnumerable<CatalogType> projectFrom, IMapper mapper)
            => projectFrom.Select(x => x.MapToCatalogTypeDto(mapper)).ToList();
    }
}