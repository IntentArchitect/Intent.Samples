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
    public class OrderSummaryDto : IMapFrom<Order>
    {
        public OrderSummaryDto()
        {
            Date = null!;
            Status = null!;
        }

        public int OrderNumber { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
        public decimal Total { get; set; }

        public static OrderSummaryDto Create(int orderNumber, string date, string status, decimal total)
        {
            return new OrderSummaryDto
            {
                OrderNumber = orderNumber,
                Date = date,
                Status = status,
                Total = total
            };
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderSummaryDto>()
                .ForMember(d => d.OrderNumber, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.Date, opt => opt.MapFrom(src => src.OrderDate))
                .ForMember(d => d.Status, opt => opt.MapFrom(src => src.OrderStatus))
                .ForMember(d => d.Total, opt => opt.MapFrom(src => src.GetTotal()));
        }
    }
}