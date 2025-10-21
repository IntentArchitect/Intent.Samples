using System;
using System.Collections.Generic;
using System.Linq;
using eShop.Ordering.Domain.BuyerAggregate;
using eShop.Ordering.Domain.Common;
using eShop.Ordering.Domain.Common.Exceptions;
using eShop.Ordering.Domain.Events;
using eShop.Ordering.Domain.OrderAggregate;
using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace eShop.Ordering.Domain.OrderAggregate
{
    public class Order : IHasDomainEvent
    {
        private List<OrderItem> _orderItems = [];

        public Order(string userId, string userName, int? buyerId, Address address)
        {
            BuyerId = buyerId;
            Address = address;
            OrderDate = DateTime.Now.ToString();
            DomainEvents.Add(new OrderStartedDomainEvent(
                order: this,
                userId: userId,
                userName: userName));
        }

        /// <summary>
        /// Required by Entity Framework.
        /// </summary>
        protected Order()
        {
            OrderDate = null!;
            Address = null!;
        }

        public int Id { get; private set; }

        public string OrderDate { get; private set; }

        public int? BuyerId { get; private set; }

        public OrderStatus OrderStatus { get; private set; }

        public string? Description { get; private set; }

        public bool IsDraft { get; private set; }

        public virtual IReadOnlyCollection<OrderItem> OrderItems
        {
            get => _orderItems.AsReadOnly();
            private set => _orderItems = new List<OrderItem>(value);
        }

        public Address Address { get; private set; }

        public virtual Buyer? Buyer { get; private set; }

        public List<DomainEvent> DomainEvents { get; set; } = [];

        public static Order NewDraft()
        {
            // [IntentFully]
            // TODO: Implement NewDraft (Order) functionality
            throw new NotImplementedException("Replace with your implementation...");
        }

        public void AddOrderItem(
            int productId,
            string productName,
            decimal unitPrice,
            decimal discount,
            string pictureUrl,
            int units = 1)
        {
            var existingOrderForProduct = _orderItems.SingleOrDefault(o => o.ProductId == productId);

            if (existingOrderForProduct != null)
            {
                //if previous line exist modify it with higher discount and units..

                if (discount > existingOrderForProduct.GetCurrentDiscount())
                {
                    existingOrderForProduct.SetNewDiscount(discount);
                }

                existingOrderForProduct.AddUnits(units);
            }
            else
            {
                //add validated new order item

                var orderItem = new OrderItem(productId, productName, unitPrice, discount, pictureUrl, units);
                _orderItems.Add(orderItem);
            }
        }

        public void SetBuyerId(int id)
        {
            BuyerId = id;
        }

        public void SetAwaitingValidationStatus()
        {
            //[IntentIgnore(Match = "DomainEvents")]
            if (OrderStatus == OrderStatus.Submitted)
            {
                DomainEvents.Add(new OrderStatusChangedToAwaitingValidationDomainEvent(this));
                OrderStatus = OrderStatus.AwaitingValidation;
            }
        }

        public void SetStockConfirmedStatus()
        {
            //[IntentIgnore(Match = "DomainEvents")]
            if (OrderStatus == OrderStatus.AwaitingValidation)
            {
                DomainEvents.Add(new OrderStatusChangedToStockConfirmedDomainEvent(this));

                OrderStatus = OrderStatus.StockConfirmed;
                Description = "All the items were confirmed with available stock.";
            }
        }

        public void SetShippedStatus()
        {
            if (OrderStatus != OrderStatus.Paid)
            {
                ThrowStatusChangeException(OrderStatus.Shipped);
            }

            OrderStatus = OrderStatus.Shipped;
            Description = "The order was shipped.";
            DomainEvents.Add(new OrderShippedDomainEvent(
                order: this));
        }

        public void SetCancelledStatus()
        {
            if (OrderStatus == OrderStatus.Paid ||
                OrderStatus == OrderStatus.Shipped)
            {
                ThrowStatusChangeException(OrderStatus.Cancelled);
            }

            OrderStatus = OrderStatus.Cancelled;
            Description = $"The order was cancelled.";
            DomainEvents.Add(new OrderCancelledDomainEvent(
                order: this));
        }

        public void SetCancelledStatusWhenStockIsRejected(IEnumerable<int> orderStockRejectedItems)
        {
            if (OrderStatus == OrderStatus.AwaitingValidation)
            {
                OrderStatus = OrderStatus.Cancelled;

                var itemsStockRejectedProductNames = OrderItems
                    .Where(c => orderStockRejectedItems.Contains(c.ProductId))
                    .Select(c => c.ProductName);

                var itemsStockRejectedDescription = string.Join(", ", itemsStockRejectedProductNames);
                Description = $"The product items don't have stock: ({itemsStockRejectedDescription}).";
            }
        }

        public decimal GetTotal()
        {
            return _orderItems.Sum(o => o.Units * o.UnitPrice);
        }

        [IntentIgnore]
        public decimal GetTotalExVat()
        {
            // [IntentFully]
            throw new NotImplementedException("Replace with your implementation...");
        }

        private void AddOrderStartedDomainEvent(string userId, string userName, string cardNumber,
            string cardSecurityNumber, string cardHolderName, DateTime cardExpiration)
        {
            var orderStartedDomainEvent = new OrderStartedDomainEvent(this, userId, userName);

            this.DomainEvents.Add(orderStartedDomainEvent);
        }

        private void ThrowStatusChangeException(OrderStatus orderStatusToChange)
        {
            //throw new OrderingDomainException($"Is not possible to change the order status from {OrderStatus} to {orderStatusToChange}.");
        }
    }
}