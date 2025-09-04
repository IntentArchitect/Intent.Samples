using System;
using eShop.Basket.Domain.Entities;
using eShop.Basket.Domain.Repositories.Documents;
using Intent.RoslynWeaver.Attributes;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.MongoDb.MongoDbDocument", Version = "1.0")]

namespace eShop.Basket.Infrastructure.Persistence.Documents
{
    internal class BasketItemDocument : IBasketItemDocument
    {
        public string Id { get; set; } = default!;
        public int ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public decimal UnitPrice { get; set; }
        public decimal OldUnitPrice { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; } = default!;

        public BasketItem ToEntity(BasketItem? entity = default)
        {
            entity ??= new BasketItem();

            entity.Id = Id ?? throw new Exception($"{nameof(entity.Id)} is null");
            entity.ProductId = ProductId;
            entity.ProductName = ProductName ?? throw new Exception($"{nameof(entity.ProductName)} is null");
            entity.UnitPrice = UnitPrice;
            entity.OldUnitPrice = OldUnitPrice;
            entity.Quantity = Quantity;
            entity.PictureUrl = PictureUrl ?? throw new Exception($"{nameof(entity.PictureUrl)} is null");

            return entity;
        }

        public BasketItemDocument PopulateFromEntity(BasketItem entity)
        {
            Id = entity.Id;
            ProductId = entity.ProductId;
            ProductName = entity.ProductName;
            UnitPrice = entity.UnitPrice;
            OldUnitPrice = entity.OldUnitPrice;
            Quantity = entity.Quantity;
            PictureUrl = entity.PictureUrl;

            return this;
        }

        public static BasketItemDocument? FromEntity(BasketItem? entity)
        {
            if (entity is null)
            {
                return null;
            }

            return new BasketItemDocument().PopulateFromEntity(entity);
        }
    }
}