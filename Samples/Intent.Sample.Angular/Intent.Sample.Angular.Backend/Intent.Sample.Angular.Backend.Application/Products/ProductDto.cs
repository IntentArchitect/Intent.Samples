using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Products
{
    public class ProductDto
    {
        public ProductDto()
        {
            Name = null!;
            Description = null!;
            Code = null!;
            BrandName = null!;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public Guid BrandId { get; set; }
        public string BrandName { get; set; }

        public static ProductDto Create(Guid id, string name, string description, string code, Guid brandId, string brandName)
        {
            return new ProductDto
            {
                Id = id,
                Name = name,
                Description = description,
                Code = code,
                BrandId = brandId,
                BrandName = brandName
            };
        }
    }
}