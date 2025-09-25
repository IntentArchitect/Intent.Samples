using System;
using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace eShop.Catalog.Domain.Entities
{
    public class CatalogItem
    {
        public CatalogItem()
        {
            Name = null!;
            Description = null!;
            PictureFileName = null!;
            PictureUri = null!;
            CatalogBrand = null!;
            CatalogType = null!;
        }
        public int Id { get; set; }

        public int CatalogBrandId { get; set; }

        public int CatalogTypeId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string PictureFileName { get; set; }

        public string PictureUri { get; set; }

        public int AvailableStock { get; set; }

        public int RestockThreshold { get; set; }

        public int MaxStockThreshold { get; set; }

        public bool OnReorder { get; set; }

        public virtual CatalogBrand CatalogBrand { get; set; }

        public virtual CatalogType CatalogType { get; set; }

        public int RemoveStock(int quantityDesired)
        {
            // [IntentFully]
            // TODO: Implement RemoveStock (CatalogItem) functionality
            throw new NotImplementedException("Replace with your implementation...");
        }

        public int AddStock(int quantity)
        {
            // [IntentFully]
            // TODO: Implement AddStock (CatalogItem) functionality// TODO: Implement AddStock (CatalogItem) functionality// TODO: Implement AddStock (CatalogItem) functionality// TODO: Implement AddStock (CatalogItem) functionality// TODO: Implement AddStock (CatalogItem) functionality// TODO: Implement RemoveStock (CatalogItem) functionality
            throw new NotImplementedException("Replace with your implementation...");
        }
    }
}