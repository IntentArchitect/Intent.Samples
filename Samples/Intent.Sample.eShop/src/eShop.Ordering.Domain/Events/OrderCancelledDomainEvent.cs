using System;
using System.Collections.Generic;
using eShop.Ordering.Domain.Common;
using eShop.Ordering.Domain.OrderAggregate;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.DomainEvents.DomainEvent", Version = "1.0")]

namespace eShop.Ordering.Domain.Events
{
    public class OrderCancelledDomainEvent : DomainEvent
    {
        public OrderCancelledDomainEvent(Order order)
        {
            Order = order;
        }

        public Order Order { get; }
    }
}