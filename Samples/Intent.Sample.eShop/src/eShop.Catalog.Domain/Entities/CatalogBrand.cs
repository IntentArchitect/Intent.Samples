using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace eShop.Catalog.Domain.Entities
{
    public class CatalogBrand
    {
        public CatalogBrand()
        {
            Brand = null!;
        }
        public int Id { get; set; }

        public string Brand { get; set; }
    }
}