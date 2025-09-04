using System.Collections.Generic;
using AutoMapper;
using eShop.Basket.Application.Common.Mappings;
using eShop.Basket.Domain.Entities;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace eShop.Basket.Application.CustomerBaskets
{
    public class BasketDto : IMapFrom<CustomerBasket>
    {
        public BasketDto()
        {
            BuyerId = null!;
            BasketItems = null!;
        }

        public string BuyerId { get; set; }
        public List<BasketItemDto> BasketItems { get; set; }

        public static BasketDto Create(string buyerId, List<BasketItemDto> basketItems)
        {
            return new BasketDto
            {
                BuyerId = buyerId,
                BasketItems = basketItems
            };
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerBasket, BasketDto>()
                .ForMember(d => d.BasketItems, opt => opt.MapFrom(src => src.BasketItems));
        }
    }
}