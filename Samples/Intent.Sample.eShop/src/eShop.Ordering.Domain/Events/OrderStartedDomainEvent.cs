using System;
using System.Collections.Generic;
using eShop.Ordering.Domain.Common;
using eShop.Ordering.Domain.OrderAggregate;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.DomainEvents.DomainEvent", Version = "1.0")]

namespace eShop.Ordering.Domain.Events
{
    public class OrderStartedDomainEvent : DomainEvent
    {
        public OrderStartedDomainEvent(Order order, string userId, string userName)
        {
            Order = order;
            UserId = userId;
            UserName = userName;
        }

        public Order Order { get; }

        public string UserId { get; }
        public string UserName { get; }
    }
}