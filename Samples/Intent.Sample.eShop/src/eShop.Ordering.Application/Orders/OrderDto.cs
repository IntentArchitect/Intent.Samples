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
    public class OrderDto : IMapFrom<Order>
    {
        public OrderDto()
        {
            Date = null!;
            Status = null!;
            Street = null!;
            City = null!;
            State = null!;
            Country = null!;
            ZipCode = null!;
            OrderItems = null!;
        }

        public int OrderNumber { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public List<OrderItemReturnDto> OrderItems { get; set; }
        public decimal Total { get; set; }

        public static OrderDto Create(
            int orderNumber,
            string date,
            string status,
            string street,
            string city,
            string state,
            string country,
            string zipCode,
            List<OrderItemReturnDto> orderItems,
            decimal total)
        {
            return new OrderDto
            {
                OrderNumber = orderNumber,
                Date = date,
                Status = status,
                Street = street,
                City = city,
                State = state,
                Country = country,
                ZipCode = zipCode,
                OrderItems = orderItems,
                Total = total
            };
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderDto>()
                .ForMember(d => d.OrderNumber, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.Date, opt => opt.MapFrom(src => src.OrderDate))
                .ForMember(d => d.Status, opt => opt.MapFrom(src => src.OrderStatus))
                .ForMember(d => d.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(d => d.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(d => d.State, opt => opt.MapFrom(src => src.Address.State))
                .ForMember(d => d.Country, opt => opt.MapFrom(src => src.Address.Country))
                .ForMember(d => d.ZipCode, opt => opt.MapFrom(src => src.Address.ZipCode))
                .ForMember(d => d.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
                .ForMember(d => d.Total, opt => opt.MapFrom(src => src.GetTotal()));
        }
    }
}