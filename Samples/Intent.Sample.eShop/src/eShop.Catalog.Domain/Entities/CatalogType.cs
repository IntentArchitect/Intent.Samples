using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace eShop.Catalog.Domain.Entities
{
    public class CatalogType
    {
        public CatalogType()
        {
            Type = null!;
        }
        public int Id { get; set; }

        public string Type { get; set; }
    }
}