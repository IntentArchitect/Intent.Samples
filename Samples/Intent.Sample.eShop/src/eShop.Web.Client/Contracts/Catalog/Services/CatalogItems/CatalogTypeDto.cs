using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.Blazor.HttpClients.DtoContract", Version = "2.0")]

namespace eShop.Web.Client.Contracts.Catalog.Services.CatalogItems
{
    public class CatalogTypeDto
    {
        public CatalogTypeDto()
        {
            Type = null!;
        }

        public int Id { get; set; }
        public string Type { get; set; }

        public static CatalogTypeDto Create(int id, string type)
        {
            return new CatalogTypeDto
            {
                Id = id,
                Type = type
            };
        }
    }
}