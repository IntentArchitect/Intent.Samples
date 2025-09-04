using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using eShop.Catalog.Domain.Entities;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.AutoMapper.MappingExtensions", Version = "1.0")]

namespace eShop.Catalog.Application.CatalogItems
{
    public static class CatalogBrandDtoMappingExtensions
    {
        public static CatalogBrandDto MapToCatalogBrandDto(this CatalogBrand projectFrom, IMapper mapper)
            => mapper.Map<CatalogBrandDto>(projectFrom);

        public static List<CatalogBrandDto> MapToCatalogBrandDtoList(this IEnumerable<CatalogBrand> projectFrom, IMapper mapper)
            => projectFrom.Select(x => x.MapToCatalogBrandDto(mapper)).ToList();
    }
}