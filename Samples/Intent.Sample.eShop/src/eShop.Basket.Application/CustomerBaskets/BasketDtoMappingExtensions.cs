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
    public static class BasketDtoMappingExtensions
    {
        public static BasketDto MapToBasketDto(this CustomerBasket projectFrom, IMapper mapper)
            => mapper.Map<BasketDto>(projectFrom);

        public static List<BasketDto> MapToBasketDtoList(this IEnumerable<CustomerBasket> projectFrom, IMapper mapper)
            => projectFrom.Select(x => x.MapToBasketDto(mapper)).ToList();
    }
}