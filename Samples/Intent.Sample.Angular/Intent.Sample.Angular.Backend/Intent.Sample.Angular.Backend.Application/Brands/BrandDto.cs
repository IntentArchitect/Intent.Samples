using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Brands
{
    public class BrandDto
    {
        public BrandDto()
        {
            Name = null!;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public static BrandDto Create(Guid id, string name)
        {
            return new BrandDto
            {
                Id = id,
                Name = name
            };
        }
    }
}