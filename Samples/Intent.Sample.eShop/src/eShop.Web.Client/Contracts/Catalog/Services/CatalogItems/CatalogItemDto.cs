using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.Blazor.HttpClients.DtoContract", Version = "2.0")]

namespace eShop.Web.Client.Contracts.Catalog.Services.CatalogItems
{
    public class CatalogItemDto
    {
        public CatalogItemDto()
        {
            Name = null!;
            Description = null!;
            PictureFileName = null!;
            PictureUri = null!;
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

        public static CatalogItemDto Create(
            int id,
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
            return new CatalogItemDto
            {
                Id = id,
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