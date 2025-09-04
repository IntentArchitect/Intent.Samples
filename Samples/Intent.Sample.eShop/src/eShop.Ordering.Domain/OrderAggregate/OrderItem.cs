using System;
using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace eShop.Ordering.Domain.OrderAggregate
{
    public class OrderItem
    {
        public OrderItem(int productId, string productName, decimal unitPrice, decimal discount, string pictureUrl, int units = 1)
        {
            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
            Discount = discount;
            PictureUrl = pictureUrl;
            Units = units;
        }

        /// <summary>
        /// Required by Entity Framework.
        /// </summary>
        protected OrderItem()
        {
            ProductName = null!;
            PictureUrl = null!;
            Order = null!;
        }
        public int Id { get; private set; }

        public int OrderId { get; private set; }

        public string ProductName { get; private set; }

        public string PictureUrl { get; private set; }

        public decimal UnitPrice { get; private set; }

        public decimal Discount { get; private set; }

        public int Units { get; private set; }

        public int ProductId { get; private set; }

        public virtual Order Order { get; private set; }

        public decimal GetCurrentDiscount()
        {
            // [IntentFully]
            // TODO: Implement GetCurrentDiscount (OrderItem) functionality
            throw new NotImplementedException("Replace with your implementation...");
        }

        public void SetNewDiscount(decimal discount)
        {
            Discount = discount;
        }

        public void AddUnits(int units)
        {
            Units = units;
        }
    }
}