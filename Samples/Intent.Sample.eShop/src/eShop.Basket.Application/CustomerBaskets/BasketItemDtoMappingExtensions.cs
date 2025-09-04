using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using AutoMapper;
using eShop.Basket.Domain.Entities;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.AutoMapper.MappingExtensions", Version = "1.0")]

namespace eShop.Basket.Application.CustomerBaskets
{
    public static class BasketItemDtoMappingExtensions
    {
        public static BasketItemDto MapToBasketItemDto(this BasketItem projectFrom, IMapper mapper)
            => mapper.Map<BasketItemDto>(projectFrom);

        public static List<BasketItemDto> MapToBasketItemDtoList(this IEnumerable<BasketItem> projectFrom, IMapper mapper)
            => projectFrom.Select(x => x.MapToBasketItemDto(mapper)).ToList();
    }
}