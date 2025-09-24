using System.Collections.Generic;
using eShop.Ordering.Application.Common.Interfaces;
using eShop.Ordering.Application.Common.Security;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace eShop.Ordering.Application.Orders.CreateOrder
{
    [Authorize]
    public class CreateOrderCommand : IRequest<int>, ICommand
    {
        public CreateOrderCommand(string userId,
            string userName,
            string street,
            string city,
            string state,
            string country,
            string zipCode,
            List<OrderItemDto> orderItems,
            int buyerId)
        {
            UserId = userId;
            UserName = userName;
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
            OrderItems = orderItems;
            BuyerId = buyerId;
        }
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string ZipCode { get; set; }

        public string Street { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public int BuyerId { get; set; }

    }
}