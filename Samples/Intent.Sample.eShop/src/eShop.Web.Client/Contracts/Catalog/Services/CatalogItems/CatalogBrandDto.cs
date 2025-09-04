using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.Blazor.HttpClients.DtoContract", Version = "2.0")]

namespace eShop.Web.Client.Contracts.Catalog.Services.CatalogItems
{
    public class CatalogBrandDto
    {
        public CatalogBrandDto()
        {
            Brand = null!;
        }

        public int Id { get; set; }
        public string Brand { get; set; }

        public static CatalogBrandDto Create(int id, string brand)
        {
            return new CatalogBrandDto
            {
                Id = id,
                Brand = brand
            };
        }
    }
}