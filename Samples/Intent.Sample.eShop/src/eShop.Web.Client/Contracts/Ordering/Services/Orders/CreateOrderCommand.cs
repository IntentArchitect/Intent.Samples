using System.Collections.Generic;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.Blazor.HttpClients.DtoContract", Version = "2.0")]

namespace eShop.Web.Client.Contracts.Ordering.Services.Orders
{
    public class CreateOrderCommand
    {
        public CreateOrderCommand()
        {
            UserId = null!;
            UserName = null!;
            Street = null!;
            City = null!;
            State = null!;
            Country = null!;
            ZipCode = null!;
            OrderItems = [];
        }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public int BuyerId { get; set; }

        public static CreateOrderCommand Create(
            string userId,
            string userName,
            string street,
            string city,
            string state,
            string country,
            string zipCode,
            List<OrderItemDto> orderItems,
            int buyerId)
        {
            return new CreateOrderCommand
            {
                UserId = userId,
                UserName = userName,
                Street = street,
                City = city,
                State = state,
                Country = country,
                ZipCode = zipCode,
                OrderItems = orderItems,
                BuyerId = buyerId
            };
        }
    }
}