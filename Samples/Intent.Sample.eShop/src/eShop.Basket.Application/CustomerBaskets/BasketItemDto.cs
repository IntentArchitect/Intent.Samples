using System;
using System.Collections.Generic;
using AutoMapper;
using eShop.Basket.Application.Common.Mappings;
using eShop.Basket.Domain.Entities;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace eShop.Basket.Application.CustomerBaskets
{
    public class BasketItemDto : IMapFrom<BasketItem>
    {
        public BasketItemDto()
        {
            Id = null!;
            ProductName = null!;
            PictureUrl = null!;
        }

        public string Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal OldUnitPrice { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; }

        public static BasketItemDto Create(
            string id,
            int productId,
            string productName,
            decimal unitPrice,
            decimal oldUnitPrice,
            int quantity,
            string pictureUrl)
        {
            return new BasketItemDto
            {
                Id = id,
                ProductId = productId,
                ProductName = productName,
                UnitPrice = unitPrice,
                OldUnitPrice = oldUnitPrice,
                Quantity = quantity,
                PictureUrl = pictureUrl
            };
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<BasketItem, BasketItemDto>();
        }
    }
}