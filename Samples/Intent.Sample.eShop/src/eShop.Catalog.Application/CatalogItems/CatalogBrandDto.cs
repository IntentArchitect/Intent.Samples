using AutoMapper;
using eShop.Catalog.Application.Common.Mappings;
using eShop.Catalog.Domain.Entities;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace eShop.Catalog.Application.CatalogItems
{
    public class CatalogBrandDto : IMapFrom<CatalogBrand>
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

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CatalogBrand, CatalogBrandDto>();
        }
    }
}