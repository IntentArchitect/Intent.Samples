using System;
using System.Collections.Generic;
using AutoMapper;
using eShop.Ordering.Application.Common.Mappings;
using eShop.Ordering.Domain.OrderAggregate;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace eShop.Ordering.Application.Orders
{
    public class OrderItemReturnDto : IMapFrom<OrderItem>
    {
        public OrderItemReturnDto()
        {
            ProductName = null!;
            PictureUrl = null!;
        }

        public string ProductName { get; set; }
        public int Units { get; set; }
        public decimal UnitPrice { get; set; }
        public string PictureUrl { get; set; }

        public static OrderItemReturnDto Create(string productName, int units, decimal unitPrice, string pictureUrl)
        {
            return new OrderItemReturnDto
            {
                ProductName = productName,
                Units = units,
                UnitPrice = unitPrice,
                PictureUrl = pictureUrl
            };
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OrderItem, OrderItemReturnDto>();
        }
    }
}