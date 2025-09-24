using System;
using System.Collections.Generic;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace eShop.Catalog.Application.CatalogItems
{
    public class CatalogItemCreateDto
    {
        public CatalogItemCreateDto()
        {
            Name = null!;
            Description = null!;
            PictureFileName = null!;
            PictureUri = null!;
        }

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

        public static CatalogItemCreateDto Create(
            int catalogBrandId,
            int catalogTypeId,
            string name,
            string description,
            decimal price,
            string pictureFileName,
            string pictureUri,
            int availableStock,
            int restockThreshold,
            int maxStockThreshold,
            bool onReorder)
        {
            return new CatalogItemCreateDto
            {
                CatalogBrandId = catalogBrandId,
                CatalogTypeId = catalogTypeId,
                Name = name,
                Description = description,
                Price = price,
                PictureFileName = pictureFileName,
                PictureUri = pictureUri,
                AvailableStock = availableStock,
                RestockThreshold = restockThreshold,
                MaxStockThreshold = maxStockThreshold,
                OnReorder = onReorder
            };
        }
    }
}