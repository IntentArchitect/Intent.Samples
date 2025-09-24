using AutoMapper;
using eShop.Catalog.Application.Common.Mappings;
using eShop.Catalog.Domain.Entities;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace eShop.Catalog.Application.CatalogItems
{
    public class CatalogTypeDto : IMapFrom<CatalogType>
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

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CatalogType, CatalogTypeDto>();
        }
    }
}