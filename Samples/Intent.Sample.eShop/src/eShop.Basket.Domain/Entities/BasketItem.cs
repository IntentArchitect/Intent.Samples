using System;
using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace eShop.Basket.Domain.Entities
{
    public class BasketItem
    {
        private string? _id;

        public BasketItem()
        {
            Id = null!;
            ProductName = null!;
            PictureUrl = null!;
        }

        public string Id
        {
            get => _id ??= Guid.NewGuid().ToString();
            set => _id = value;
        }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal OldUnitPrice { get; set; }

        public int Quantity { get; set; }

        public string PictureUrl { get; set; }
    }
}